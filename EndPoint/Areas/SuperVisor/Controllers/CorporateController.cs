using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class CorporateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
