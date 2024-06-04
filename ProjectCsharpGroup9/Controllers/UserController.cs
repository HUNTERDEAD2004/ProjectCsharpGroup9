using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
	public class UserController : Controller
	{
		AppDbContext _dbContext;
		public UserController()
		{
			_dbContext = new AppDbContext();
		}
        public IActionResult SignUp() // view đăng ký
        {
            return View();
        }
        [HttpPost]
		public IActionResult SignUp(User user) // action đăng ký
		{
			try
			{
				user.UserID = Guid.NewGuid();
				user.RoleID = 2;
				_dbContext.Users.Add(user);
				//đăng ký thì sẽ tạo luôn giỏ hàng
				var CartUser = new Cart()
				{
					CartID = user.UserID,
					UserID = user.UserID,
					CreateDay = DateTime.Now
				};
				var Mem = new Membership()
				{
					MemberID = user.UserID,
					UserID = user.UserID,
					Point = 0,
					Status = "0",
					MemberShipRank = "Iron"
				};
				_dbContext.Memberships.Add(Mem);
				_dbContext.Carts.Add(CartUser);
				_dbContext.SaveChanges();
				TempData["status"] = "Tạo tài khoản thành công";
				return RedirectToAction("Login");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
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
					return RedirectToAction("Index", "Home");
				}
			}
		}
		public IActionResult LogOut() //action đăng xuất
		{
			HttpContext.Session.Remove("user");
			return RedirectToAction("Login");
		}
	}
}
