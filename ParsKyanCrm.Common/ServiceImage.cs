using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public static class ServiceImage
    {
        public static Image LoadBase64(string base64)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SaveImageByByte_InExistNextDelete(string source, string imgpath, string oldImage, string strMessage)
        {
            try
            {



                string base64 = source.Substring(source.IndexOf(',') + 1);
                base64 = base64.Trim('\0');

                //For Security ==> Only Image
                LoadBase64(base64);

                byte[] chartData = Convert.FromBase64String(base64);

                if (chartData.Length > 5000000) return "" + strMessage + " باید کمتر از 5 مگ باشد";

                FileOperation.DeleteFile(oldImage);
                System.IO.File.WriteAllBytes(imgpath, chartData);

                return string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertImageToByte(string imgpath)
        {
            try
            {
                if (!FileOperation.ExistsFile(imgpath)) return string.Empty;

                byte[] imageArray = System.IO.File.ReadAllBytes(imgpath);
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageArray));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
