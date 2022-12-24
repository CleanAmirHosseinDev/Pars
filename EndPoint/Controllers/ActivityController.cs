using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
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
            return View();
        }


    }
}
