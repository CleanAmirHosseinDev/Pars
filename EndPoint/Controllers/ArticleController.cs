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

        public async Task<IActionResult> ContentList(string s = null, int offset = 1) {

            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto() {
                    KindOfContent = 62,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 1000,
                    PageIndex = offset
                };
                var content = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);
                ViewData["content"] = content.Data;
            } catch(Exception ex) {
                return Redirect("/Error/Code404");
            }
            return View();
        }
        public async Task<IActionResult> Content(int? id = null) {

            try {
                ViewBag.id = id;
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                request.ContentId = id;
                var content = await _basicInfoFacad.GetNewsAndContentService.Execute(request);
                if(content != null) {
                    ViewData["content"] = content;
                } else {
                    throw new EntryPointNotFoundException();
                }
            } catch(Exception e) {
                return Redirect("/Error/Code404?fromlink=/Article/Content/" + id);
            }

            ViewData["NewNews"] = Task.Run(getSideNewsList).Result;
            ViewData["NewContent"] = Task.Run(getSideContentList).Result;

            return View();
        }

        public async Task<IActionResult> NewsList(string s = null, int offset = 1) {

            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto() { 
                    KindOfContent = 61,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 1000,
                    PageIndex = offset
                };
                var news = await _basicInfoFacad.GetNewsAndContentsService.Execute(request);
                ViewData["news"] = news.Data;
            } catch(Exception ex) {
                return Redirect("/Error/Code404");
            }

            return View();
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
                return Redirect("/Error/Code404?fromlink=/Article/News/" + id);
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

        [Route("Page/{dlink?}")]//PageController Mixed with Action
        public async Task<IActionResult> Page(string dlink = null) {
            if(dlink==null) return Redirect("/Error/Code404?fromlink=/Page/" + dlink);

            int id = 0;
            if(dlink != null && dlink.Length>0){
                char ch1 = dlink.ToCharArray()[0];
                if(ch1>='0' && ch1<='9') {
                    id = Convert.ToInt32(dlink);
                }
            }


            try {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                if(id != 0) {
                    request.ContentId = id;
                } else {
                    request.DirectLink = dlink;
                }
                var news = await _basicInfoFacad.GetNewsAndContentService.Execute(request);
                if(news != null) {
                    if(news.KindOfContent == 61) {
                        return RedirectPermanent("/Article/News/" + news.ContentId);
                    }
                    if(news.KindOfContent == 62) {
                        return RedirectPermanent("/Article/Content/" + news.ContentId);
                    }
                    if(id!=0 && news.DirectLink!=null && news.DirectLink.Length > 0){
                        return RedirectPermanent("/Page/" + news.DirectLink);
                    }
                    ViewData["news"] = news;
                } else {
                    return Redirect("/Error/Code404?fromlink=/Page/" + dlink);
                }
            } catch(Exception e) {
                return Redirect("/Error/Code404?fromlink=/Page/" + dlink);
            }



            ViewData["NewNews"] = Task.Run(getSideNewsList).Result;
            ViewData["NewContent"] = Task.Run(getSideContentList).Result;

            return View();
        }



    }
}
