using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Customer.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
