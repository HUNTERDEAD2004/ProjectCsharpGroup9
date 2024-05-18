using Microsoft.AspNetCore.Mvc;
using ProjectCsharpGroup9.Models;
using System.Diagnostics;

namespace ProjectCsharpGroup9.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
