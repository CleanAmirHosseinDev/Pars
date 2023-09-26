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

    public class ActivityController : Controller
    {

        private readonly ILogger<ActivityController> _logger;
        private readonly IUserFacad _userFacad;
        public ActivityController(ILogger<ActivityController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        public async Task<IActionResult> Index(int? id = null)
        {
            try
            {
                ViewBag.id = id;
                ViewData["data"] = await _userFacad.GetActivityService.Execute(new RequestActivityDto() { ActivityId = id });
                ViewData["NewNews"] = await getSideNewsList();
                ViewData["NewContent"] = await getSideContentList();
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<NewsAndContentDto>> getSideNewsList()
        {
            try
            {
                return( await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
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
                throw;
            }
        }


    }
}
