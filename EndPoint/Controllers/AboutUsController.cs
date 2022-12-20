using EndPoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<AboutUsController> _logger;

        public AboutUsController(ILogger<AboutUsController> logger)
        {
            _logger = logger;
        }

      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VisionAndMission()
        {
            return View();
        }
        public IActionResult OrganazationChart()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
