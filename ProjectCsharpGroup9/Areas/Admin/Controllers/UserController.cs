using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;
using System.Drawing.Printing;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        AppDbContext _dbContext;
        public UserController()
        {
            _dbContext = new AppDbContext();
        }
        [Route("Index")]
        public IActionResult Index(string find) // danh sách tài khoản
        {
            var a = _dbContext.Users.ToList();
            if(string.IsNullOrEmpty(find)) return View(a);
            else
            {
                var findData = _dbContext.Users.Where(p=>p.UserName.Contains(find)).ToList();
                if(findData.Count == 0) return View(a);
                else return View(findData);
            }
        }
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id) // view sửa tài khoản
        {
            var a = _dbContext.Users.Find(id);
            return View(a);
        }
        [HttpPost]
        public IActionResult Edit(User user)//action sửa tài khoản
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)//action xóa tài khoản
        {
            var a = _dbContext.Users.Find(id);
            _dbContext.Users.Remove(a);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
