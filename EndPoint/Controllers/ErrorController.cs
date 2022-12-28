using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Code404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
