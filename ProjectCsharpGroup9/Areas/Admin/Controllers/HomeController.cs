using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Home")]
    public class HomeController : Controller
    {
        AppDbContext _dbContext;
        public HomeController()
        {
                _dbContext = new AppDbContext();
        }
        public IActionResult Index()
        {
          var a =  _dbContext.Products.Select(p => new { Sales = p.BillDetails.Sum(bd => bd.Quantity) }).OrderByDescending(s => s.Sales).Take(5);
            return View();
        }
    }
}
