using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
    public class CartDetailController : Controller
    {
        HttpClient _client = new HttpClient();
        public IActionResult Index()
        {
            
            
            var loginData = HttpContext.Session.GetString("user");
            if (loginData == null) return RedirectToAction("Login","User");
            var user = JsonConvert.DeserializeObject<User>(loginData);
            string Url = $@"https://localhost:7276/api/CartDetail/Get-Cart-User?UserID={user.UserID}";
            var response = _client.GetStringAsync(Url).Result;
            List<CartDetails> cartDetails = JsonConvert.DeserializeObject<List<CartDetails>>(response);
            return View(cartDetails);
        }
        public ActionResult RemoveFromCart(Guid id)
        {
            string Url = $@"https://localhost:7276/api/CartDetail/Remove-From-Cart?id={id}";
            var response = _client.GetStringAsync(Url).Result;
            return RedirectToAction("Index");
        }
        public ActionResult CheckOut()
        {
            var loginData = HttpContext.Session.GetString("user");
            if (loginData == null) return RedirectToAction("Login","User");
            var user = JsonConvert.DeserializeObject<User>(loginData);
            string Url = $@"https://localhost:7276/api/CartDetail/CheckOut?UserID={user.UserID}";
            var response = _client.GetStringAsync(Url).Result;
            return RedirectToAction("Index");
        }
    }
}
