using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class RequestForRatingController : BaseController
    {
        public IActionResult Index(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public IActionResult RequestReferences(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

    }
}
