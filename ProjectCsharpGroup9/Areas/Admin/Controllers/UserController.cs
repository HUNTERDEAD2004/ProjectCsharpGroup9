using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        HttpClient _client = new HttpClient();
        public UserController()
        {
            _dbContext = new AppDbContext();
        }
        [Route("Index")]
        public IActionResult Index(string find) // danh sách tài khoản
        {
            string Url = $@"https://localhost:7276/api/User/Get-All-User";
            var response = _client.GetStringAsync(Url).Result;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);

            if (string.IsNullOrEmpty(find)) return View(users);
            else
            {
                var findData = users.Where(p=>p.UserName.Contains(find)).ToList();
                return View(findData);
            }
        }
        public void GetRole(int selected)
        {
            var role = _dbContext.Roles.ToList();
            SelectList listItems = new SelectList(role, "RoleID", "RoleName", selected);
            ViewBag.RoleID = listItems;
        }
        [HttpGet("{id}")]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id) // view sửa tài khoản
        {
            GetRole(0);
            string Url = $@"https://localhost:7276/api/User/Get-ID-User?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            User user = JsonConvert.DeserializeObject<User>(response);
            return View(user);
        }
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(User user)//action sửa tài khoản
        {
            string Url = $@"https://localhost:7276/api/User/Edit-User";
            var response = _client.PutAsJsonAsync(Url, user).Result;
            //Console.WriteLine(response);
            return RedirectToAction("Index");
        }
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)//action xóa tài khoản
        {
            string Url = $@"https://localhost:7276/api/User/Delete-User?id={id}";
            var response = await _client.DeleteAsync(Url);
            return RedirectToAction("Index");
        }
    }
}
