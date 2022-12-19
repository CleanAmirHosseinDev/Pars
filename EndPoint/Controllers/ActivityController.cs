using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
   
    public class ActivityController : Controller
    {

        private readonly ILogger<ActivityController> _logger;
        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }


    }
}
