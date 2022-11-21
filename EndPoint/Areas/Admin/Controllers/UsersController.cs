using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditUser(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult EditUserAccessLevel(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
