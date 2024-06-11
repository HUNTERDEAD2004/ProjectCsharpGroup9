using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GalleryController : ControllerBase
	{
		AppDbContext _dbContext;

		public GalleryController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet("GetAll")]
		public ActionResult<IEnumerable<Gallery>> GetAll()
		{
			return _dbContext.Gallerys.ToList();
		}

		[HttpGet("GetById")]
		public ActionResult<Gallery> GetById(Guid id)
		{
			var gallery = _dbContext.Gallerys.Find(id);

			if (gallery == null)
			{
				return NotFound();
			}

			return gallery;
		}

		[HttpPost("Create")]
		public ActionResult<Gallery> Create(Gallery gallery)
		{
			try
			{
				_dbContext.Gallerys.Add(gallery);
				_dbContext.SaveChanges();

				return CreatedAtAction(nameof(GetById), new { id = gallery.GalleryID }, gallery);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("Update")]
		public IActionResult Update(Gallery gallery)
		{
			_dbContext.Entry(gallery).State = EntityState.Modified;

			try
			{
				_dbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GalleryExists(gallery.GalleryID))
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

		[HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			var gallery = _dbContext.Gallerys.Find(id);
			if (gallery == null)
			{
				return NotFound();
			}

			_dbContext.Gallerys.Remove(gallery);
			_dbContext.SaveChanges();

			return NoContent();
		}

		private bool GalleryExists(Guid id)
		{
			return _dbContext.Gallerys.Any(e => e.GalleryID == id);
		}
	}
}