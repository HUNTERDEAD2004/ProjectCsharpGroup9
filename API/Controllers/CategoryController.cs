using API.IService;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<Category> _CategoryService;
        DbSet<Category> _Category;
        public CategoryController()
        {
            _dbContext = new AppDbContext();
            _Category = _dbContext.Categories;
            _CategoryService = new Service<Category>(_Category, _dbContext);
        }
        [HttpGet("Get-All-Category")]
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetAll());
        }
        [HttpGet("Get-ID-Category")]
        public ActionResult GetByID(Guid id)
        {
            return Ok(_CategoryService.GetByID(id));
        }
        [HttpPost("Create-Category")]
        public ActionResult Create(Category category)
        {
            if (_CategoryService.Create(category)) return Ok();
            else return BadRequest();
        }
        [HttpPut("Edit-Category")]
        public ActionResult Edit(Category category)
        {
            if (_CategoryService.Upate(category)) return Ok();
            else return BadRequest();
        }
        [HttpDelete("Delete-Category")]
        public ActionResult Delete(Guid id)
        {
            if (_CategoryService.Delete(id)) return Ok();
            else return BadRequest();
        }

    }
}
