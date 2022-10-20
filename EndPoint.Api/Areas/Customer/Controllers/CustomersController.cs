using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Customer.Controllers
{
    public class CustomersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
