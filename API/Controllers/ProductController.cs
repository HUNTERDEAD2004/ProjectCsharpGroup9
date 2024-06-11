using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<Product> _ProductService;
        DbSet<Product> _Product;
        ProductService _Service;
        IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = new AppDbContext();
            _Product = _dbContext.Products;
            _ProductService = new Service<Product>(_Product, _dbContext);
            _Service = new ProductService();
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Get-All-Product")]
        public ActionResult GetAll()
        {
            return Ok(_ProductService.GetAll());
        }

        [HttpGet("Get-Id-product")]
        public ActionResult<Product> GetById(Guid id)
        {
            return Ok(_ProductService.GetByID(id));
        }

        [HttpPost("Create-Product")]
        public ActionResult Create([FromForm] Product product)
        {
            if (_Service.Create(product)) return Ok();
            else return BadRequest();
        }

        [HttpPut("edit-Product")]
        public IActionResult Update(Product product)
        {
            if (_ProductService.Upate(product)) return Ok();
            else return BadRequest();
        }

        [HttpDelete("Delete-Product")]
        public IActionResult Delete(Guid id)
        {
            if (_ProductService.Delete(id)) return Ok();
            else return BadRequest();
        }
    }
}
