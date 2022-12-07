using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class ManagerOfParsKyanController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditManagerOfParsKyan(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}

