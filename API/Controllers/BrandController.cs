using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<Brand> _BrandService;
        DbSet<Brand> _Brand;
        public BrandController()
        {
            _dbContext = new AppDbContext();
            _Brand = _dbContext.Brands;
            _BrandService = new Service<Brand>(_Brand, _dbContext);
        }
        [HttpGet("Get-All-Brand")]
        public ActionResult GetAll()
        {
            return Ok(_BrandService.GetAll());
        }
        [HttpGet("Get-ID-Brand")]
        public ActionResult GetByID(Guid id)
        {
            return Ok(_BrandService.GetByID(id));
        }
        [HttpPost("Create-Brand")]
        public ActionResult Create(Brand brand)
        {
            if (_BrandService.Create(brand)) return Ok();
            else return BadRequest();
        }
        [HttpPut("Edit-Brand")]
        public ActionResult Edit(Brand brand)
        {
            if (_BrandService.Upate(brand)) return Ok();
            else return BadRequest();
        }
        [HttpDelete("Delete-Brand")]
        public ActionResult Delete(Guid id)
        {
            if (_BrandService.Delete(id)) return Ok();
            else return BadRequest();
        }
    }
}
