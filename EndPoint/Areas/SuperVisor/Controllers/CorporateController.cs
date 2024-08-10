using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class CorporateController : BaseController
    {
        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult ShowScore(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
