using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
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
            return View(products);
        }
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
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
    }
}