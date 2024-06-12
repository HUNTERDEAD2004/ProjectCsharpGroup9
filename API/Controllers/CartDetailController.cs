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
        public ActionResult GetCartUser(Guid UserID)
        {
            return Ok(_Service.GetCartUser(UserID));
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
    }
}
