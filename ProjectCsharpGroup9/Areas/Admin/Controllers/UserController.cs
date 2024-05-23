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
        [HttpGet]
        public IActionResult Index() // danh sách tài khoản
        {
            var a = _dbContext.Users.ToList();
            return View(a);
        }
        [Route("SignUp")]
        public IActionResult SignUp() // view đăng ký
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user) // action đăng ký
        {
            try
            {
                _dbContext.Users.Add(user);
                //đăng ký thì sẽ tạo luôn giỏ hàng
                var CartUser = new Cart()
                {
                    CartID = user.UserID,
                    UserID = user.UserID,
                    CreateDay = DateTime.Now
                };
                _dbContext.Carts.Add(CartUser);
                _dbContext.SaveChanges();
                TempData["status"] = "Tạo tài khoản thành công";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("Login")]
        public IActionResult Login(string username, string password) //action đăng nhập
        {
            if (username == null && password == null) { return View(); }
            else
            {
                var data = _dbContext.Users.FirstOrDefault(p => p.UserName == username && p.Password == password);
                if (data == null)
                {
                    TempData["status"] = "Đăng nhập thất bại XD";
                    return RedirectToAction("Login");
                }
                else
                {
                    var jsonData = JsonConvert.SerializeObject(data);
                    HttpContext.Session.SetString("user", jsonData);
                    return RedirectToAction("Home", "Admin");
                }
            }
        }
        public IActionResult LogOut() //action đăng xuất
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
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
