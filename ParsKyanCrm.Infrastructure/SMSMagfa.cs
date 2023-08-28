using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public class SMSMagfa
    {
        public async Task<string> MagfaSendSMS()
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

    }
}
