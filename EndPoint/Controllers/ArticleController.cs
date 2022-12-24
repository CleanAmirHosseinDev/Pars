using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers {
    public class ArticleController :Controller {

        private readonly ILogger<ArticleController> _logger;
        private IBasicInfoFacad _basicInfoFacad;
        public ArticleController(ILogger<ArticleController> logger, IBasicInfoFacad basicInfoFacad) {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<IActionResult> News(int? id = null) {
            try {
                ViewBag.id = id;
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.ContentId = id;
                var news = await _basicInfoFacad.GetNewsAndContentService.Execute(request);
                if(news != null) {
                    ViewData["news"] = news;
                } else {
                    throw new EntryPointNotFoundException();
                }
            } catch(Exception e) {
                var data = new { message = "NewsNotFound" };
                return new NotFoundObjectResult(data);
            }
            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 5;
                request.PageIndex = 1;
                var news = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);

                ViewData["NewNews"] = news.Data;
            } catch(Exception ex) {

            }

            return View();
        }

        public async Task<IActionResult> NewsList(string s = null,int offset=1) {

            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                request.PageSize = 20;
                request.PageIndex = offset;
                var news = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);
                ViewData["news"] = news.Data;
            } catch(Exception ex) {
                var data = new { message = "NewsNotFound" };
                return new NotFoundObjectResult(data);
            }

            return View();
        }


    }
}
