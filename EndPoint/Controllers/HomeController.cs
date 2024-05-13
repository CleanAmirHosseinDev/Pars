using EndPoint.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static ParsKyanCrm.Infrastructure.CaptchaService;

namespace EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserFacad _userFacad;

        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IUserFacad userFacad, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            _userFacad = userFacad;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 4;
                request.KindOfContent = 61;
                request.PageIndex = 1;
                var news = await _userFacad.GetNewsAndContentsService.Execute(request);

                ViewData["news"] = news.Data;

                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 4;
                request.KindOfContent = 62;
                request.PageIndex = 1;
                var content = await _userFacad.GetNewsAndContentsService.Execute(request);

                ViewData["content"] = content.Data;

                RequestRankingOfCompaniesDto request2 = new RequestRankingOfCompaniesDto();
                request2.IsActive = (byte)TablesGeneralIsActive.Active;
                request2.PageSize = 10;
                request2.PageIndex = 1;
                var ranks = await _userFacad.GetRankingOfCompaniessService.Execute(request2);

                ViewData["ranks"] = ranks.Data;
            }
            catch (Exception ex)
            {
                var x = ex;
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RankList()
        {

            return View();
        }

        public async Task<IActionResult> RankList_Data(RequestRankingOfCompaniesDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 10000;
                request.PageIndex = 1;
                ViewBag.CustomerID = request.CustomerID;
                return PartialView("P_RankList", (await _userFacad.GetRankingOfCompaniessService.Execute(request)).Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("file-upload")]
        public async Task<IActionResult> Upload(IFormFile upload)
        {

            try
            {
                string url = VaribleForName.CkeditorFolder + upload.FileName;
                await ServiceFileUploader.SaveFile(upload, _env.ContentRootPath + url, "فایل");

                var success = new { uploaded = 1, upload.FileName, url = url.Replace("/wwwroot", string.Empty), error = new { message = "آپلود با موفقیت انجام شد" } };
                return Json(success);
            }
            catch (Exception ex)
            {
                var success = new { uploaded = 0, error = new { message = ex.Message } };
                return Json(success);
            }

        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [CaptchaCheck]
        [HttpPost]
        public async Task<JsonResult> Register_Save([FromForm] Customers_RegisterLandingDto form)
        {
            var error = "";
            if (ModelState.IsValid)
            {
                return Json(await _userFacad.SaveCustomers_RegisterLandingService.Execute(form));
            }
            else
            {
                foreach (var item in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList()) error += item.First().ErrorMessage + " ";

                return Json(new ResultDto() { IsSuccess = false, Message = error });
            }

        }

        public async Task<string> MagfaSendSMS(string message, string mobile)
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
                messages = new[] { message },
                recipients = new[] { mobile }
            }
            );

            // Call
            var response = await client.PostAsync(request);
            return response.Content;
            // return View();
        }


        public IActionResult Assessment(string survey8Y77dBBS1)
        {
            ViewBag.sdklsdslkdcmskdlsdljcee4389eijdks = survey8Y77dBBS1;
            return View();
        }

        [HttpPost]
        [CaptchaCheck]
        public async Task<JsonResult> AssessmentP([FromBody] RequestSaveAssessmentDto request)
        {
            try
            {
                var error = "";
                if (ModelState.IsValid)
                {
                    return Json(await _userFacad.SaveAssessmentService.Execute(request));
                }
                else
                {
                    foreach (var item in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList()) error += item.First().ErrorMessage + " ";

                    return Json(new ResultDto() { IsSuccess = false, Message = error });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
