using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginRequest(ModelLoginView model)
        {
            string result = string.Empty;
            if (model != null)
            {
                if (model.Userneme == "1" && model.Password == "1")
                {
                    result = "/Admin/Home/Index";
                }
                return Ok(new JsonResultViewModel()
                {
                    ResultCode = 200,
                    Data = result
                });
            }
            else
            {
               
                return BadRequest(new JsonResultViewModel()
                {
                    ResultCode = 404,
                    Data = result
                });
            }

        }
    }
}
