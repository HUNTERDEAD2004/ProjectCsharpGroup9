using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _dbContext;

        public ProductController()
        {
            _dbContext = new AppDbContext();    
        }

        public ActionResult Index()
        {
            var GetAll = _dbContext.Products.Include(p => p.Galleries).ToList();
            return View(GetAll);
        }

        public ActionResult Details(Guid ProductId)
        {
            var GetDetails = _dbContext.Products.Find(ProductId);
            return View(GetDetails);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult Edit(Guid ProductId)
        {
            var GetEdit = _dbContext.Products.Find(ProductId);
            return View(GetEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                var GetEdit = _dbContext.Products.Find(product.ProductID);
                GetEdit.ProductName = product.ProductName;
                GetEdit.InputDay = product.InputDay;
                GetEdit.RegularPrice = product.RegularPrice;
                GetEdit.DiscountPrice = product.DiscountPrice;
                GetEdit.Quantity = product.Quantity;
                GetEdit.ProductWeight = product.ProductWeight;
                GetEdit.Description = product.Description;
                _dbContext.Products.Update(GetEdit);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult Delete(Guid ProductId)
        {
            try
            {
                var GetDelete = _dbContext.Products.Find(ProductId);
                _dbContext.Products.Remove(GetDelete);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
