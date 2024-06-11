using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http;

namespace ProjectCsharpGroup9.Controllers
{
	public class UserController : Controller
	{
		AppDbContext _dbContext;
		HttpClient _client = new HttpClient();
		public UserController()
		{
			_dbContext = new AppDbContext();
		}
        public IActionResult SignUp() // view đăng ký
        {
            return View();
        }
        [HttpPost("User/Create-User")]
		public ActionResult SignUp(User user) // action đăng ký
		{
			try
			{
                user.UserID = Guid.NewGuid();
                user.RoleID = 2;
                string Url = $@"https://localhost:7276/api/User/Create-User";
				var response = _client.PostAsJsonAsync(Url, user).Result;
				if (response.IsSuccessStatusCode)
				{
                    TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
                    return RedirectToAction("Login");
                }
				else
				{
					return View();
				}

            }
			catch (Exception)
			{
				return BadRequest();
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
