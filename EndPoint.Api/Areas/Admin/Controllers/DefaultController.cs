using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Api.Areas.Admin.Controllers
{
    
    public class DefaultController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
