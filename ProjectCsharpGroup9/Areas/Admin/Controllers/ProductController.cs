using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        AppDbContext _dbContext;
        HttpClient _client = new HttpClient();
        public ProductController()
        {
            _dbContext = new AppDbContext();
        }
        [HttpGet("Index")]
        public ActionResult Index()
        {
            string Url = $@"https://localhost:7276/api/Product/Get-All-Product";
            var response = _client.GetStringAsync(Url).Result;
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(response);
            return View(products);
        }
        [HttpGet("Detail/{id}")]
        public ActionResult Details(Guid id)
        {
            string Url = $@"https://localhost:7276/api/Product/Get-ID-Product?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            Product products = JsonConvert.DeserializeObject<Product>(response);
            return View(products);
        }
        [HttpGet("Create")]
        public ActionResult Create()
        {
            var categories = GetCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name");
            var brands = GetBrands();
            ViewBag.brands = new SelectList(brands, "BrandID", "Name");
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (_dbContext.Products.Any(u => u.ProductName == product.ProductName))
            {
                ModelState.AddModelError("ProductName", "Đã tồn tại sản phẩm");
                return View();
            }
            product.InputDay = DateTime.Now;
            string Url = $@"https://localhost:7276/api/Product/Create-Product";
            var response = _client.PostAsJsonAsync(Url, product).Result;
            return RedirectToAction("Index");
        }
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(Guid id)
        {
            string Url = $@"https://localhost:7276/api/Product/Get-ID-Product?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            Product products = JsonConvert.DeserializeObject<Product>(response);
            var categories = GetCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name", products.CategoryId);
            var brands = GetBrands();
            ViewBag.brands = new SelectList(brands, "BrandID", "Name", products.BrandID);
            return View(products);
        }
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (_dbContext.Products.Any(u => u.ProductName == product.ProductName))
            {
                ModelState.AddModelError("ProductName", "Đã tồn tại sản phẩm");
                return View();
            }
            string Url = $@"https://localhost:7276/api/Product/Edit-Product";
            var response = _client.PutAsJsonAsync(Url, product).Result;
            return RedirectToAction("Index");
        }
        [Route("Delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            string Url = $@"https://localhost:7276/api/Product/Delete-Product?id={id}";
            var response = _client.DeleteAsync(Url).Result;
            return RedirectToAction("Index");
        }
        private List<Category> GetCategories()
        {
            string url = $"https://localhost:7276/api/Category/Get-All-Category";
            var response = _client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        private List<Brand> GetBrands()
        {
            string url = $"https://localhost:7276/api/Brand/Get-All-Brand";
            var response = _client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<Brand>>(response);
        }
    }
}