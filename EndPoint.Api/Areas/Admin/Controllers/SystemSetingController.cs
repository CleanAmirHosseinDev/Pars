using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Admin.Controllers
{
    public class SystemSetingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
