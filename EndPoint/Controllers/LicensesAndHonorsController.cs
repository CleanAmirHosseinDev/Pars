using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
   
    public class LicensesAndHonorsController : Controller
    {

        private readonly ILogger<LicensesAndHonorsController> _logger;
        public LicensesAndHonorsController(ILogger<LicensesAndHonorsController> logger)
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
