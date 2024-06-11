using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;

        public ProductController()
        {
            _db = new AppDbContext();
        }

        public ActionResult Index()
        {
            var GetAll = _db.Products.Include(p => p.Galleries).ToList();
            return View(GetAll);
        }

        public ActionResult Details(Guid ProductId)
        {
            var GetDetails = _db.Products.Find(ProductId);
            return View(GetDetails);
        }
    }
}
