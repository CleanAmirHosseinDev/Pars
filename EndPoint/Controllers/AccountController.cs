﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index( ) {            
            return View();
        }
        public IActionResult LoginUser( ) {
            return View();
        }

        public IActionResult Login(string u = null)
        {
            ViewBag.u = u;
            return View();
        }

    }
}
