using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class FutherInfoController : BaseController
    {
        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

    }
}
