using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class ArticleController : Controller
    {

        private readonly ILogger<ArticleController> _logger;
        private IUserFacad _userFacad;
        public ArticleController(ILogger<ArticleController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        public async Task<IActionResult> ContentList(string s = null, int offset = 1)
        {

            try
            {
                ViewData["content"] = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
                {
                    KindOfContent = 62,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 1000,
                    PageIndex = offset
                })).Data;
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<IActionResult> Content(int? id = null)
        {

            try
            {
                ViewBag.id = id;
                var content = await _userFacad.GetNewsAndContentService.Execute(new RequestNewsAndContentDto()
                {
                    ContentId = id
                });
                if (content != null && content.ContentId > 0) ViewData["content"] = content;
                else throw new EntryPointNotFoundException();

                ViewData["NewNews"] = await getSideNewsList();
                ViewData["NewContent"] = await getSideContentList();

                return View();

            }
            catch (Exception e)
            {
                return Redirect("/Error/Code404?fromlink=/Article/Content/" + id);
            }
        }

        public async Task<IActionResult> NewsList(string s = null, int offset = 1)
        {

            try
            {

                ViewData["news"] = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
                {
                    KindOfContent = 61,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 1000,
                    PageIndex = offset
                })).Data;
            }
            catch (Exception ex)
            {
                return Redirect("/Error/Code404");
            }

            return View();
        }
        public async Task<IActionResult> News(int? id = null)
        {

            try
            {
                ViewBag.id = id;
                var news = await _userFacad.GetNewsAndContentService.Execute(new RequestNewsAndContentDto() { ContentId = id });
                if (news != null && news.ContentId > 0) ViewData["news"] = news;
                else throw new EntryPointNotFoundException();

                ViewData["NewNews"] = Task.Run(getSideNewsList).Result;
                ViewData["NewContent"] = Task.Run(getSideContentList).Result;

                return View();

            }
            catch (Exception e)
            {
                return Redirect("/Error/Code404?fromlink=/Article/News/" + id);
            }


        }

        private async Task<IEnumerable<NewsAndContentDto>> getSideNewsList()
        {
            try
            {
                return (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
                {
                    KindOfContent = 61,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 5,
                    PageIndex = 1
                })).Data;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        private async Task<IEnumerable<NewsAndContentDto>> getSideContentList()
        {
            try
            {
                return (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
                {
                    KindOfContent = 62,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 5,
                    PageIndex = 1
                })).Data;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [Route("Page/{dlink?}")]//PageController Mixed with Action
        public async Task<IActionResult> Page(string dlink = null)
        {
            if (dlink == null) return Redirect("/Error/Code404?fromlink=/Page/" + dlink);

            int id = 0;
            if (dlink != null && dlink.Length > 0)
            {
                char ch1 = dlink.ToCharArray()[0];
                if (ch1 >= '0' && ch1 <= '9')
                {
                    id = Convert.ToInt32(dlink);
                }
            }


            try
            {
                RequestNewsAndContentDto request = new RequestNewsAndContentDto();
                if (id != 0)
                {
                    request.ContentId = id;
                }
                else
                {
                    request.DirectLink = dlink;
                }
                var news = await _userFacad.GetNewsAndContentService.Execute(request);
                if (news == null || news.ContentId == 0)
                {
                    news.Title = "صفحه یافت نشد";
                    news.Body = $"صفحه‌ای با مسیر {{{dlink}}} یافت نشد!";
                    Response.StatusCode = 404;
                }
                else
                {
                    if (news.KindOfContent == 61)
                    {
                        return RedirectPermanent("/Article/News/" + news.ContentId);
                    }
                    if (news.KindOfContent == 62)
                    {
                        return RedirectPermanent("/Article/Content/" + news.ContentId);
                    }
                    if (id != 0 && news.DirectLink != null && news.DirectLink.Length > 0)
                    {
                        return RedirectPermanent("/Page/" + news.DirectLink);
                    }
                }
                ViewData["news"] = news;
            }
            catch (Exception e)
            {
                return Redirect("/Error/Code404?fromlink=/Page/" + dlink + "?e=AEE");
            }



            ViewData["NewNews"] = Task.Run(getSideNewsList).Result;
            ViewData["NewContent"] = Task.Run(getSideContentList).Result;

            return View();
        }
        // This function defines how to get a value from search input, find the result, and return it.
        public async Task<IActionResult> Search(string search = null, int offset = 1)
        {
            try
            {
                var content = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
                {
                    //KindOfContent = 62,
                    IsActive = (byte)TablesGeneralIsActive.Active,
                    PageSize = 1000,
                    PageIndex = offset
                })).Data;
                ViewData["query"] = search;
                ViewData["content"] = content.Where(x=> (x.Title.Contains(search) || x.Body.Contains(search)) && (x.KindOfContent == 61 || x.KindOfContent == 62)).Distinct();
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
