using Microsoft.AspNetCore.Mvc;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
