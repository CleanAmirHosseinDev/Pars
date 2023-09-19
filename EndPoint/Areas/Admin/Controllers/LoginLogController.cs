using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    public class LoginLogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
