using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<CartDetails> _CartDetailService;
        DbSet<CartDetails> _CartDetail;
        CartDetailService _Service;
        public CartDetailController()
        {
            _dbContext = new AppDbContext();
            _CartDetail = _dbContext.CartsDetails;
            _CartDetailService = new Service<CartDetails>(_CartDetail, _dbContext);
            _Service = new CartDetailService();
        }
        [HttpGet("Get-All-CartDetail")]
        public ActionResult GetAll()
        {
            return Ok(_CartDetailService.GetAll());
        }
        [HttpGet("Get-Cart-User")]
        public ActionResult<IEnumerable<CartDetails>> GetCartUser(Guid UserID)
        {
            try
            {
                var cartDetails = _Service.GetCartUser(UserID);
                

                // Load related product information
                foreach (var cartDetail in cartDetails)
                {
                    cartDetail.Product = _dbContext.Products.FirstOrDefault(p => p.ProductID == cartDetail.ProductID);
                }

                return Ok(cartDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("Remove-From-Cart")]
        public ActionResult RemoveFromCart(Guid id)
        {
            if (_Service.RemoveFromCart(id)) return Ok();
            else return BadRequest();
        }
        [HttpGet("CheckOut")]
        public ActionResult Checkout(Guid UserID)
        {
            if(_Service.CheckOut(UserID)) return Ok(); 
            else return BadRequest();
        }
		[HttpPost("Update-CartDetail")]
		public ActionResult UpdateCartDetail([FromBody] CartDetails cartDetail)
		{
			try
			{
				var existingCartDetail = _dbContext.CartsDetails.FirstOrDefault(cd => cd.CartDetailID == cartDetail.CartDetailID);
				if (existingCartDetail == null)
				{
					return NotFound($"Cart detail with ID {cartDetail.CartDetailID} not found.");
				}

				existingCartDetail.Quantity = cartDetail.Quantity;
				_dbContext.SaveChanges();

				return Ok(existingCartDetail);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
