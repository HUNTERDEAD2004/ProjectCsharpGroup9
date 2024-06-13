using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller
    {
        AppDbContext _dbContext;
        HttpClient _client = new HttpClient();
        public BrandController()
        {
            _dbContext = new AppDbContext();
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            string Url = $@"https://localhost:7276/api/Brand/Get-All-Brand";
            var response = _client.GetStringAsync(Url).Result;
            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(response);
            return View(brands);
        }
        [HttpGet("Detail")]
        public ActionResult Detail(Guid id)
        {
            string Url = $@"https://localhost:7276/api/Brand/Get-ID-Brand?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            Brand brand = JsonConvert.DeserializeObject<Brand>(response);
            return View(brand);
        }
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        public ActionResult Create(Brand brand, IFormFile icon)
        {
            try
            {
                if (_dbContext.Brands.Any(u => u.Name == brand.Name))
                {
                    ModelState.AddModelError("Name", "Đã tồn tại");
                    return View();
                }
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "icon", icon.FileName);
                var stream = new FileStream(path, FileMode.Create);
                {
                    icon.CopyTo(stream);
                }
                brand.BrandID = Guid.NewGuid();
                brand.Icon = icon.FileName;
                string Url = $@"https://localhost:7276/api/Brand/Create-Brand";
                var response = _client.PostAsJsonAsync(Url, brand).Result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(Guid id)
        {
            string Url = $@"https://localhost:7276/api/Brand/Get-ID-Brand?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            Brand brand = JsonConvert.DeserializeObject<Brand>(response);
            return View(brand);
        }
        [HttpPost("Edit/{id}")]
        public ActionResult Edit(Brand brand, IFormFile icon)
        {
            try
            {
                //var existingBrand = _dbContext.Brands.Find(brand.BrandID);
                //if (existingBrand == null) return NotFound();
                //// Cập nhật các thuộc tính khác của brand nếu có
                //existingBrand.Name = brand.Name;
                //existingBrand.Description = brand.Description;
                if (_dbContext.Brands.Any(u => u.Name == brand.Name))
                {
                    ModelState.AddModelError("Name", "Đã tồn tại");
                    return View();
                }
                if (icon != null && icon.Length > 0)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "icon", icon.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        icon.CopyTo(stream);
                    }
                    //existingBrand.Icon = icon.FileName;
                    brand.Icon = icon.FileName;
                }
                //_dbContext.Brands.Update(existingBrand);
                //_dbContext.SaveChanges();
                string Url = $@"https://localhost:7276/api/Brand/Edit-Brand";
                var response = _client.PutAsJsonAsync(Url, brand).Result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("Delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                string Url = $@"https://localhost:7276/api/Brand/Delete-Brand?id={id}";
                var response = _client.DeleteAsync(Url).Result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
