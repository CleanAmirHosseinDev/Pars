using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Customer.Controllers
{
    public class FurtherInfoController : BaseController
    {
        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult NotAccess()
        {
            return View();
        }

    }
}
