using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Services.WebService;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class CityController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditCity(int? id = null)
        {
            ViewBag.id = id;           
            return View();
        }

      
    }
}
