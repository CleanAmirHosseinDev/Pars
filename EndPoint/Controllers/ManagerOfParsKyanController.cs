using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
   
    public class ManagerOfParsKyanController : Controller
    {

        private readonly ILogger<ManagerOfParsKyanController> _logger;
        public ManagerOfParsKyanController(ILogger<ManagerOfParsKyanController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }
       

    }
}
