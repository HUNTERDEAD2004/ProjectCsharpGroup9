using Microsoft.AspNetCore.Mvc;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
