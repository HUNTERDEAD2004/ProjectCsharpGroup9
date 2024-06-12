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
    public class BillController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<Bill> _BillService;
        DbSet<Bill> _bills;
        BillServices _Service;
        public BillController()
        {
            _dbContext = new AppDbContext();
            _bills = _dbContext.Bills;
            _BillService = new Service<Bill>(_bills, _dbContext);
            _Service = new BillServices();
        }
        [HttpGet("Get-All-Bill")]
        public ActionResult GetAll()
        {
            return Ok(_BillService.GetAll());
        }
        [HttpGet("Get-ID-Bill")]
        public ActionResult GetByID(Guid id)
        {
            return Ok(_BillService.GetByID(id));
        }
        [HttpGet("Buy-Again")]
        public ActionResult BuyAgain(Guid id)
        {
            if (_Service.BuyAgain(id)) return Ok();
            else return BadRequest();
        }
        [HttpGet("Cancel-Payment")]
        public ActionResult CancelPayment(Guid Id)
        {
            if (_Service.CancelPayment(Id)) return Ok();
            else return BadRequest();
        }
        [HttpGet("Get-Bills")]
        public ActionResult GetBills(Guid UserID)
        {
            return Ok(_Service.GetBills(UserID));
        }
        [HttpGet("Get-BillDetails")]
        public ActionResult GetBillDetails(Guid UserID, Guid BillID)
        {
            return Ok(_Service.GetBillsDetail(UserID,BillID));
        }
    }
}
