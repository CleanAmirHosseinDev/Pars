using EndPoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBasicInfoFacad _basicInfoFacad;

        public HomeController(ILogger<HomeController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<IActionResult> Index()
        {
            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 4;
                request.KindOfContent = 61;
                request.PageIndex = 1;
                var news = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);

                ViewData["news"] = news.Data;

                RequestRankingOfCompaniesDto request2 = new RequestRankingOfCompaniesDto();
                request2.IsActive = (byte)TablesGeneralIsActive.Active;
                request2.PageSize = 6;
                request2.PageIndex = 1;
                var ranks = await _basicInfoFacad.GetRankingOfCompaniessService.Execute(request2);

                ViewData["ranks"] = ranks.Data;
            } catch(Exception ex) {
                var x = ex;
            }
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> RankList( ) {
            try {

                RequestRankingOfCompaniesDto request2 = new RequestRankingOfCompaniesDto();
                request2.IsActive = (byte)TablesGeneralIsActive.Active;
                request2.PageSize = 6;
                request2.PageIndex = 1;
                var ranks = await _basicInfoFacad.GetRankingOfCompaniessService.Execute(request2);

                ViewData["ranks"] = ranks.Data;
            } catch(Exception ex) {
                var x = ex;
            }
            return View();
        }
    }
}
