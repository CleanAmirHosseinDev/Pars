using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        public IActionResult UserReport()
        {
            return View();
        }
    }
}
