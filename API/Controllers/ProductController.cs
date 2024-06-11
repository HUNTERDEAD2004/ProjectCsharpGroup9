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

        public ProductController()
        {
            _dbContext = new AppDbContext();
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _dbContext.Products.ToList();
            return Ok(products);
        }

        [HttpGet("GetById")]
        public ActionResult<Product> GetById(Guid id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("Create-Product")]
        public ActionResult<Product> Create(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Product")]
        public IActionResult Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("Delete-Product")]
        public IActionResult Delete(Guid id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _dbContext.Products.Any(e => e.ProductID == id);
        }
    }
}
