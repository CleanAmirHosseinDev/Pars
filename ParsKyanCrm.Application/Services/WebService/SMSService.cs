using Newtonsoft.Json;
using ParsKyanCrm.Infrastructure.Consts;
using SMSMagfaService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.WebService
{
    public class Parameters
    {
        //  public string username { get; set; }
        // public string password { get; set; }
        // public string from { get; set; }
        // public string to { get; set; }
        public bool farsi { get; set; }
        public string message { get; set; }

    }
    public class SMSService
    {


        public static string Execute(string MobileNumber, string Message)
        {
            try
            {
                if (VaribleForName.IsDebug == true)
                {

                    return "ارسال انجام شد";

                }
                else
                {

                    if (string.IsNullOrEmpty(MobileNumber)) return "موبایل را وارد کنید";
                    if (string.IsNullOrEmpty(Message)) return "متن پیام را وارد کنید";

                    #region رهیاب

                    Parameters param = new Parameters();
                    param.message = Message;
                    string Data = JsonConvert.SerializeObject(param);
                    string result = CreateObject("https://rahyabbulk.ir:8443/url/send.ashx?username=parsmehr&password=pars562361&from=50001475&to=" + MobileNumber + "&farsi=true&message=" + Message, Data, "POST");

                    if (string.IsNullOrEmpty(result) && result != "-1")
                    {
                        return "ارسال انجام شد";
                    }
                    else
                    {
                        // return "ارسال با خطا مواجعه شد دوباره سعی کنید";
                        return "ارسال با خطا مواجعه شد دوباره سعی کنید"+result;
                    }

                    #endregion

                    #region مگفا
                    string[] num = { "300081473" };
                    string[] msg = { Message };                    
                    string[] mob = { MobileNumber };
                    //SMSMagfa(msg, num, mob);
                    #endregion

                }

                //  return result;
            }
            catch (Exception ex)
            {
                // throw ex;
                return "ارسال با خطا مواجعه شد دوباره سعی کنید";
            }

        }
        private static string CreateObject(string url, string data, string method)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("charset", "utf-8");

                if (data != null)
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(data);
                    request.ContentLength = bytes.Length;
                    using (Stream webStream = request.GetRequestStream())
                    {
                        webStream.Write(bytes, 0, bytes.Length);
                    }
                }


                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            Regex regex = new Regex("<ReturnIDs>(.*)</ReturnIDs>");
                            Regex regex2 = new Regex("<message>(.*)</message>");
                            string resID = regex.Match(response).Groups[1].ToString();

                            return response;
                        }
                    }
                    return "-1";
                }
            }
            catch (WebException e)
            {
                var reader = new StreamReader(e.Response.GetResponseStream());
                var content = reader.ReadToEnd();
                return "-1";
            }
        }

       

    }

}
