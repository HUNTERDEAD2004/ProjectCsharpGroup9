using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AppDbContext _dbContext;
        Service<User> _UserService;
        DbSet<User> _Users;
        UserService _Service;
        public UserController()
        {
            _dbContext = new AppDbContext();
            _Users = _dbContext.Users;
            _UserService = new Service<User>(_Users, _dbContext);
            _Service = new UserService();
        }

        [HttpGet("Get-All-User")]
        public ActionResult GetAll()
        {
            return Ok(_UserService.GetAll());
        }
        [HttpGet("Get-ID-User")]
        public ActionResult GetByID(Guid id)
        {
            return Ok(_UserService.GetByID(id));
        }
        [HttpPost("Create-User")]
        public ActionResult Create(User user)
        {
            var age = _Service.CalculateAge(user.BirthDay);
            if (age <= 10)
            {
                ModelState.AddModelError("Age", "Age must be greater than 10.");
                return BadRequest(ModelState);
            }
            if (_Service.Create(user)) return Ok();
            else return BadRequest();
        }

        [HttpPut("Edit-User")]
        public ActionResult Edit(User user)
        {
            var age = _Service.CalculateAge(user.BirthDay);
            if (age <= 10)
            {
                ModelState.AddModelError("Age", "Age must be greater than 10.");
                return BadRequest(ModelState);
            }
            if (_Service.Update(user)) return Ok();
            else return BadRequest();
        }
        [HttpDelete("Delete-User")]
        public ActionResult Delete(Guid id)
        {
            if (_UserService.Delete(id)) return Ok();
            else return BadRequest();
        }
    }
}
