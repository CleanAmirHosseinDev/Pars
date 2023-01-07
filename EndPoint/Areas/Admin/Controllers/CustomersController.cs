using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class CustomersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditCustomers(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}

