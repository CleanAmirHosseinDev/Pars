using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
   
    public class ActivityController : Controller
    {

        private readonly ILogger<ActivityController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public ActivityController(ILogger<ActivityController> logger,IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<IActionResult> Index(int? id = null)
        {
            ViewBag.id = id;
            try {
                var data = await _basicInfoFacad.GetActivityService.Execute(new RequestActivityDto() { ActivityId = id });
                ViewData["data"] = data;
            } catch(Exception) {
                throw;
            }

            ViewData["NewNews"] = Task.Run(getSideNewsList).Result;
            ViewData["NewContent"] = Task.Run(getSideContentList).Result;
            return View();
        }

        private async Task<IEnumerable<NewsAndContentDto>> getSideNewsList( ) {
            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto() {
                    KindOfContent = 61,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 5,
                    PageIndex = 1
                };
                var news = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);
                return news.Data;
            } catch(Exception ex) {

            }
            return null;
        }
        private async Task<IEnumerable<NewsAndContentDto>> getSideContentList( ) {
            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto() {
                    KindOfContent = 62,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 5,
                    PageIndex = 1
                };
                var content = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);
                return content.Data;
            } catch(Exception ex) {

            }
            return null;
        }


    }
}
