using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
    public class BillController : Controller
    {
        HttpClient _client = new HttpClient();
        public IActionResult Index()
        {
            var loginData = HttpContext.Session.GetString("user");
            if(loginData == null) return RedirectToAction("Login","User");
            var user = JsonConvert.DeserializeObject<User>(loginData);
            string Url = $@"https://localhost:7276/api/Bill/Get-Bills?UserID={user.UserID}";
            var response = _client.GetStringAsync(Url).Result;
            List<Bill> categories = JsonConvert.DeserializeObject<List<Bill>>(response);
            return View(categories);
        }
        public ActionResult BillDetail(Guid BillID)
        {
            var loginData = HttpContext.Session.GetString("user");
            if(loginData == null) return RedirectToAction("login", "User");
            var user = JsonConvert.DeserializeObject<User>(loginData);
            string Url = $@"https://localhost:7276/api/Bill/Get-BillDetails?UserID={user.UserID}&BillID={BillID}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            if (bill == null) return NotFound();
            return View(bill);
        }
        public ActionResult BuyAgain(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-ID-Bill?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            return View(bill);
        }
        public ActionResult CancelPayment(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-ID-Bill?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            return View(bill);
        }
    }
}
