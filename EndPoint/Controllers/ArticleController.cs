using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class ArticleController : Controller
    {

        private readonly ILogger<ArticleController> _logger;
        public ArticleController(ILogger<ArticleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }


    }
}
