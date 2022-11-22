using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.WebService
{
   public class SMSService
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

        public string SendSMS(string MobileNumber,string Message)
        {
            Parameters param = new Parameters();
            param.message = Message;
            string Data = JsonConvert.SerializeObject(param);
            string result = CreateObject("https://rahyabbulk.ir:8443/url/send.ashx?username=parsmehr&password=pars562361&from=50001475&to="+MobileNumber+"&farsi=true&message="+Message, Data, "POST");
            return result;

        }
        private static string CreateObject(string url, string data, string method)
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

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            return response;
                        }
                    }

                }
            }
            catch (WebException e)
            {
                var reader = new StreamReader(e.Response.GetResponseStream());
                var content = reader.ReadToEnd();
                return "-1";
            }
            return "-1";
        }

    }

}
