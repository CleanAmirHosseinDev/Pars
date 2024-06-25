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
    public class CorporateController : BaseController
    {
        public IActionResult DataFormQuestions()
        {
            return View();
        }
        public IActionResult EditDataFormQuestions(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult DataFormQuestionsOptione()
        {
            return View();
        }

        public IActionResult EditDataFormQuestionsOptione()
        {
            return View();
        }
    }
}