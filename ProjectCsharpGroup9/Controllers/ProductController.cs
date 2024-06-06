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

        public IActionResult Index()
        {
            var GetAll = _db.Products.ToList();
            return View(GetAll);
        }

        //public ActionResult AddToCart()
        //{
        //    try
        //    {
        //        var GetData = _db.Products.Find();


        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
