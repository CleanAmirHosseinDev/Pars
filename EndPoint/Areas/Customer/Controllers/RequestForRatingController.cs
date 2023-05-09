using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Customer.Controllers
{
    public class RequestForRatingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ContractPrint(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult RequestDocument(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult PrintFactor(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult ContractPrinting(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult EditRequestForRating(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        

        public IActionResult EditContract(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult ConfirmContract(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult Referral(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult RequestReferences(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
