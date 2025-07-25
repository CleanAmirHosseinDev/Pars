﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class RequestForRatingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexA()
        {
            return View();
        }


        public IActionResult Referral(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult RequestDocument(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult RequestReferences(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult RequestReferencesA(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult ContractPrint(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult PrintFactor(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
