using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Admin.Controllers
{
    public class StateController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

