using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
   
    public class CareerOpportunitiesController : Controller
    {

        private readonly ILogger<CareerOpportunitiesController> _logger;
        public CareerOpportunitiesController(ILogger<CareerOpportunitiesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       

    }
}
