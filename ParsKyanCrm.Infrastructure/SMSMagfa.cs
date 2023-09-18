using magfaWebService;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public class SMSMagfa
    {
        public async Task<string> MagfaSendSMSHTTPV2()
        {
            // Credentials
            string username = "kian_81473";
            string password = "VXO6KRQFind7PZUA";

            // for vam30
            //string username = "parsmehr_71403";
            //string password = "ZTwhXDdMLDLDmpHo";
            string domain = "";

            var baseAddress = "https://sms.magfa.com/api/http/sms/v2";

            // Options
            var options = new RestClientOptions(baseAddress)
            {
                // Auth
                Authenticator = new HttpBasicAuthenticator(username + "/" + domain, password),
                ThrowOnAnyError = true
            };

            // Client
            var client = new RestClient(options);

            // Request
            var request = new RestRequest("send", Method.Post);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");

            // JSON
            request.AddBody(new
            {
                senders = new[] { "300081473" },
                messages = new[] { "test msg" },
                recipients = new[] { "09363084693" }
            }
            );

            // Call
            var response = await client.PostAsync(request);
            return  response.Content;
            // return View();
        }

      

        public async Task<string> SMSMagfaWebService(string[] Mes, string[] Number, string[] Mobile)
        {
            // سرویس soap V2 مگفا  https://messaging.magfa.com/ui/?public/wiki/api/soap_v2#toc7

            string username = "kian_81473";
            string password = "VXO6KRQFind7PZUA";
            string domain = "";

            //===============================
            // Service (Add a Web Reference)
            MagfaSoapServerClient service = new MagfaSoapServerClient();

            // Basic Auth
            NetworkCredential netCredential = new NetworkCredential(username + "/" + domain, password);

            Uri uri = new Uri("https://sms.magfa.com/api/soap/sms/v2/server");
            ICredentials credentials = netCredential.GetCredential(uri, "Basic");
          //  service.ClientCredentials = credentials;

            // SOAP Compression For .NET FrameWork 2 or later
            // service.EnableDecompression = true;
           

            sendResponse send = await service.sendAsync(Mes, Number, Mobile,
             new long?[] { },
            new int?[] { },
            new string[] { },
            new int?[] { });

            if (send.@return.status != 0)
            {
                return "-1";
            }
            else
            {
                return "1";
            }

        }

    }
}
