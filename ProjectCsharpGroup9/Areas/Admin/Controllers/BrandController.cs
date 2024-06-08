using Microsoft.AspNetCore.Mvc;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller
    {
        AppDbContext _dbContext;
        public BrandController()
        {
            _dbContext = new AppDbContext();
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var GetAll = _dbContext.Brands.ToList();
            return View(GetAll);
        }
        [HttpGet("Detail")]
        public ActionResult Detail(Guid id)
        {
            var a = _dbContext.Brands.Find(id);
            return View(a);
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
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "icon", icon.FileName);
                var stream = new FileStream(path, FileMode.Create);
                {
                    icon.CopyTo(stream);
                }
                brand.BrandID = Guid.NewGuid();
                brand.Icon = icon.FileName;
                _dbContext.Brands.Add(brand);
                _dbContext.SaveChanges();
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
            var a = _dbContext.Brands.Find(id);
            return View(a);
        }
        [HttpPost("Edit/{id}")]
        public ActionResult Edit(Brand brand, IFormFile icon)
        {
            try
            {
                var existingBrand = _dbContext.Brands.Find(brand.BrandID);
                if (existingBrand == null) return NotFound();
                // Cập nhật các thuộc tính khác của brand nếu có
                existingBrand.Name = brand.Name;
                existingBrand.Description = brand.Description;
                if (icon != null && icon.Length > 0)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "icon", icon.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        icon.CopyTo(stream);
                    }
                    existingBrand.Icon = icon.FileName;
                }
                _dbContext.Brands.Update(existingBrand);
                _dbContext.SaveChanges();
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
                var a = _dbContext.Brands.Find(id);
                _dbContext.Brands.Remove(a);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
