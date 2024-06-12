using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        HttpClient _client = new HttpClient();
        [Route("Index")]
        public ActionResult Index()
        {
            string Url = $@"https://localhost:7276/api/Category/Get-All-Category";
            var response = _client.GetStringAsync(Url).Result;
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(response);
            return View(categories);
        }
        [HttpGet("Details/{Id}")]
        public ActionResult Details(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Category/Get-ID-Category?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Category category  = JsonConvert.DeserializeObject<Category>(response);
            return View(category);
        }
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            string Url = $@"https://localhost:7276/api/Category/Create-Category";
            var response = _client.PostAsJsonAsync(Url, category).Result;
            return RedirectToAction("Index");
        }
        [HttpGet("Edit/{Id}")]
        public ActionResult Edit(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Category/Get-ID-Category?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Category category = JsonConvert.DeserializeObject<Category>(response);
            return View(category);
        }
        [HttpPost("Edit/{Id}")]
        public ActionResult Edit(Category category)
        {
            string Url = $@"https://localhost:7276/api/Category/Edit-Category";
            var response = _client.PutAsJsonAsync(Url, category).Result;
            return RedirectToAction("Index");
        }
        [Route("Delete/{Id}")]
        public ActionResult Delete(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Category/Delete-Category?id={Id}";
            var response = _client.DeleteAsync(Url).Result;
            return RedirectToAction("Index");
        }
    }
}
