using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
