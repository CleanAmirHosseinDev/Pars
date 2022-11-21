using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Customer.Controllers
{
    public class CustomerController : BaseController
    {
        public IActionResult EditCustomer()
        {
            
            return View();
        }
    }
}
