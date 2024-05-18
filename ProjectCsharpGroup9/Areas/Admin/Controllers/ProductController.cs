using Microsoft.AspNetCore.Mvc;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        [Route("Index")]
        public IActionResult Index() //Giao diện product-list
        {
            return View();
        }

        [Route("ViewCategory")]
        public IActionResult ViewCategory()
        {
            return View();
        }
    }
}
