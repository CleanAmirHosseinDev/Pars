
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ParsKyanCrm.Common.PersianNumber;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using RandomHelper;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using static ParsKyanCrm.Application.Services.WebService.CaptchaService;

namespace EndPoint {



    public class CaptchaController :Controller {

        private static IHostingEnvironment Environment;

        public CaptchaController(IHostingEnvironment _environment) {
            Environment = _environment;
        }

            public async Task<JsonResult> getCapchaImage( ) {
            try {
                bool reason = false;
                string code = RndCode.Generate(100000, 999999, "6");
                string showcode = "";
                String guid = Guid.NewGuid().ToString();

                int type = Convert.ToInt32(RndCode.Generate(0, 9, "1")); ;
                int one = Convert.ToInt32(RndCode.Generate(1, 9, "1")); ;
                int two = Convert.ToInt32(RndCode.Generate(10, 90, "1"));

                if(type > 5) {
                    showcode = two + " + " + one;
                    code = (two + one) + "";
                } else {
                    showcode = two + " - " + one;
                    code = (two - one) + "";
                }

                 Ado_NetOperation.Select("DELETE FROM CaptchaCodes WHERE Date < DATEADD(MINUTE, -10, GETDATE())");//delete old

                do {
                    code = RndCode.Generate(100000, 999999, "6"); showcode = code;// comment to enable calculator mode
                    guid = Guid.NewGuid().ToString();
                    string q = $"Insert Into CaptchaCodes (Code,Guid,Date,Usage) values('{code}','{guid}','{ DateTime.Now}','0')";
                    Ado_NetOperation.ExecuteSql(q);
                    DataTable d = Ado_NetOperation.Select($"Select * from CaptchaCodes where Guid ='{guid}'");
                    reason = d.Rows.Count != 0;
                }
                while(!reason);

                string enc = Helpers.EncryptData(code + "_" + guid);
                Bitmap SecImage = new Bitmap(Environment.WebRootPath + "/image/"+ "captcha_bg.jpg");
                Graphics graphImage = Graphics.FromImage(SecImage);
                graphImage.SmoothingMode = SmoothingMode.AntiAlias;
                Brush brush = new SolidBrush(Color.Gray);
                graphImage.DrawString(PersianNumberHelper.PersianToEnglish(" " + showcode + " "), new Font("arial", 20, FontStyle.Bold), brush, 0.1f, 4f);


                graphImage.DrawLine(new Pen(Color.Gray, 3), 0, RndCode.GenerateInt(10, 15), 200, RndCode.GenerateInt(20, 30));
                graphImage.DrawLine(new Pen(Color.Black, 2), 0, RndCode.GenerateInt(28, 38), 200, RndCode.GenerateInt(1, 2 ));
                SecImage = Blur(SecImage, 1);


                System.IO.MemoryStream ms = new MemoryStream();
                SecImage.Save(ms, ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                var base64img = Convert.ToBase64String(byteImage); // Get Base64            

                return Json(new {
                    xcode = enc,
                    image = base64img
                });
            } catch(Exception ex) {
                return null;
            }
        }

        private static Bitmap Blur(Bitmap image, Int32 blurSize) {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }

        private static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize) {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using(Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for(int xx = rectangle.X ; xx < rectangle.X + rectangle.Width ; xx++) {
                for(int yy = rectangle.Y ; yy < rectangle.Y + rectangle.Height ; yy++) {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for(int x = xx ; (x < xx + blurSize && x < image.Width) ; x++) {
                        for(int y = yy ; (y < yy + blurSize && y < image.Height) ; y++) {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for(int x = xx ; x < xx + blurSize && x < image.Width && x < rectangle.Width ; x++)
                        for(int y = yy ; y < yy + blurSize && y < image.Height && y < rectangle.Height ; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }
        public ActionResult getCaptcha( ) {
            try {
                return PartialView("~/Views/Shared/_CaptchaLayout.cshtml");
            } catch(Exception ex) {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult checkCaptcha(string captcha ) {
            if(Helpers.CaptchaCheckInDatabase(captcha)) {
                return Content("ok");
            } else {
                return Content("fail");
            }
        }
        public static string loadCaptcha( ) {
                string path = Environment.ContentRootPath + "/Views/Shared/" + "_CaptchaLayout.cshtml";
                string text = System.IO.File.ReadAllText(path);
            
            return text;
        }

    }
}