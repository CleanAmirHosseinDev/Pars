using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Supervisor.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
