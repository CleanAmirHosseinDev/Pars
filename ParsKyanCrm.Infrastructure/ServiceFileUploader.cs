using ClosedXML.Excel;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public static class ServiceFileUploader
    {
        public static string LoadBase64(string base64, string strMessage)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "لطفا " + strMessage + " به درستی بارگذاری کنید";
            }
        }
        public static string LoadBase64(Stream stream, string strMessage)
        {
            try
            {
                Image image = Image.FromStream(stream);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "لطفا " + strMessage + " به درستی بارگذاری کنید";
            }
        }

        public static void SaveImageByByte_InExistNextDelete(string source, string imgpath, string oldImage, string strMessage)
        {
            try
            {



                string base64 = source.Substring(source.IndexOf(',') + 1);
                base64 = base64.Trim('\0');

                //For Security ==> Only Image
                string strErr = LoadBase64(base64, strMessage);
                if (!string.IsNullOrEmpty(strErr)) throw new Exception(strErr);

                byte[] chartData = Convert.FromBase64String(base64);
                
                if (chartData.Length > 10000000) throw new Exception("" + strMessage + " باید کمتر از 10 مگ باشد");

                FileOperation.DeleteFile(oldImage);
                System.IO.File.WriteAllBytes(imgpath, chartData);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task SaveFile(IFormFile formFile, string imgpath, string strMessage)
        {
            try
            {

                string strErr = string.Empty;

                switch (Path.GetExtension(formFile.FileName.ToLower()).Replace(".", ""))
                {
                    case "pdf":

                        strErr = IsPdf(formFile.OpenReadStream(), strMessage);

                        break;
                    case "xlsx":
                    case "xls":

                        strErr = IsExcel(formFile.OpenReadStream(), strMessage);

                        break;
                    default:
                        strErr = LoadBase64(formFile.OpenReadStream(), strMessage);
                        break;
                }

                if (!string.IsNullOrEmpty(strErr)) throw new Exception(strErr);

                if (formFile.Length > 10000000) throw new Exception("" + strMessage + " باید کمتر از 10 مگ باشد");
               
                using (Stream fileStream = new FileStream(imgpath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

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

        public static string GetFullPath(string filename, string path, bool isImage = true)
        {
            try
            {

                string strU = path + (string.IsNullOrEmpty(filename) ? VaribleForName.No_Photo : filename);

                switch (!string.IsNullOrEmpty(filename) ? Path.GetExtension(filename.ToLower()).Replace(".", "") : string.Empty)
                {
                    case "pdf":
                    case "xlsx":
                    case "xls":

                        return strU;

                    default:

                        return isImage ? ConvertImageToByte(strU) : strU;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string IsPdf(Stream stream, string strMessage)
        {
            try
            {

                PdfReader pdfReader = new PdfReader(stream);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "لطفا " + strMessage + " پی دی اف آپلود کنید";
            }
        }

        public static string IsExcel(Stream stream, string strMessage)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(stream);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "لطفا " + strMessage + " اکسل آپلود کنید";
            }
        }

    }
}
