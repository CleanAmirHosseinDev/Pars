using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using ParsKyanCrm.Common.PersianNumber;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public class CaptchaService
    {

        public class sampleDto
        {
            public string CaptchaCodes { get; set; }
        }
        public class CaptchaCheck : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                try
                {
                    /*for(int i = 0 ; i < filterContext.ActionArguments.Count ; i++) {
                        filterContext.ActionArguments[filterContext.ActionArguments.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                    }*/

                    object a = filterContext.ActionArguments.First();
                    if (a.GetType().GetProperties().Length == 2 &&
                        a.GetType().GetProperties().Where(a => a.Name == "Value").Count() > 0)
                    {
                        a = a.GetType().GetProperties().Where(a => a.Name == "Value").First().GetValue(a);
                    }

                    var b = new sampleDto();
                    var value = "";



                    var typeOfA = a.GetType();
                    foreach (var propertyOfA in typeOfA.GetProperties())
                    {
                        if (propertyOfA.Name == "CaptchaCodes")
                        {
                            value = (string)propertyOfA.GetValue(a);
                        }
                    }




                    bool isOk = Helpers.CaptchaCheckInDatabase((string)value);

                    if (!isOk)
                    {
                        filterContext.ModelState.AddModelError("CaptchaCodes", "لطفا کد امنیتی را به درستی وارد نمایید.");
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.ToString();
                }
            }


        }

        public static class Helpers
        {
            public static bool CaptchaCheckInDatabase(string value)
            {
                bool isOk = false;
                try
                {
                    if (value != null)
                    {
                        String codes = ((String)(value)).Replace("'", "\"");
                        var json = JObject.Parse(codes);
                        String xcode = json["xcode"].ToString();
                        String code = PersianNumberHelper.PersianToEnglish(json["code"].ToString());
                        String[] clientxCode = Helpers.DecryptData(xcode).Split('_');

                        DataTable d = Ado_NetOperation.Select($"Select * from CaptchaCodes where Guid ='{clientxCode[1]}'");

                        if (d.Rows.Count > 0)
                        {
                            string tcode = (string)d.Rows[0].ItemArray[1];
                            DateTime ttime = (DateTime)d.Rows[0].ItemArray[2];
                            int usageTime = (int)d.Rows[0].ItemArray[4];
                            int maxTimeAccept = 3;

                            if (tcode == code && clientxCode[0] == code && DateTime.Now < ttime.AddMinutes(10))
                            {
                                if (usageTime < 3)
                                {
                                    Ado_NetOperation.Select($"UPDATE CaptchaCodes SET [Usage] = {usageTime + 1} where Guid ='{clientxCode[1]}'");
                                    isOk = true;
                                }
                                else
                                {
                                    Ado_NetOperation.Select($"DELETE FROM CaptchaCodes where Guid ='{clientxCode[1]}'");
                                    isOk = false;
                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
                return isOk;
            }
            private static String Encryptionkey = "$E&IcL5HO";
            public static string EncryptData(string textData)
            {

                try
                {
                    RijndaelManaged objrij = new RijndaelManaged();
                    //set the mode for operation of the algorithm
                    objrij.Mode = CipherMode.CBC;
                    //set the padding mode used in the algorithm.
                    objrij.Padding = PaddingMode.PKCS7;
                    //set the size, in bits, for the secret key.
                    objrij.KeySize = 0x80;
                    //set the block size in bits for the cryptographic operation.
                    objrij.BlockSize = 0x80;
                    //set the symmetric key that is used for encryption & decryption.
                    byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
                    //set the initialization vector (IV) for the symmetric algorithm
                    byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    int len = passBytes.Length;
                    if (len > EncryptionkeyBytes.Length)
                    {
                        len = EncryptionkeyBytes.Length;
                    }
                    Array.Copy(passBytes, EncryptionkeyBytes, len);
                    objrij.Key = EncryptionkeyBytes;
                    objrij.IV = EncryptionkeyBytes;
                    //Creates symmetric AES object with the current key and initialization vector IV.
                    ICryptoTransform objtransform = objrij.CreateEncryptor();
                    byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
                    //Final transform the test string.
                    return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            static public string DecryptData(string EncryptedText)
            {
                try
                {
                    RijndaelManaged objrij = new RijndaelManaged();
                    objrij.Mode = CipherMode.CBC;
                    objrij.Padding = PaddingMode.PKCS7;
                    objrij.KeySize = 0x80;
                    objrij.BlockSize = 0x80;
                    byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
                    byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
                    byte[] EncryptionkeyBytes = new byte[0x10];
                    int len = passBytes.Length;
                    if (len > EncryptionkeyBytes.Length)
                    {
                        len = EncryptionkeyBytes.Length;
                    }
                    Array.Copy(passBytes, EncryptionkeyBytes, len);
                    objrij.Key = EncryptionkeyBytes;
                    objrij.IV = EncryptionkeyBytes;
                    byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                    return Encoding.UTF8.GetString(TextByte);  //it will return readable string
                }
                catch (Exception ex)
                {
                    throw ex;

                }

            }
        }

    }
}
