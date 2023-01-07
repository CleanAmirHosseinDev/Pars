using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class CustomersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowCustomers(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
