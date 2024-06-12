using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;
using System.Net.WebSockets;

namespace ProjectCsharpGroup9.Controllers
{
    public class ProductController : Controller
    {
        HttpClient _client = new HttpClient();

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
        public ActionResult AddToCart(Guid id, int quantity)
        {
            var loginData = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(loginData)) return RedirectToAction("Login", "User");
            else
            {
                quantity = 1;
                var user = JsonConvert.DeserializeObject<User>(loginData);
                string Url = $@"https://localhost:7276/api/Product/Add-To-Cart?id={id}&quantity={quantity}&UserID={user.UserID}";
                var response = _client.GetStringAsync(Url).Result;
                return RedirectToAction("Index");
            }
        }
    }
}
