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
            if (loginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var user = JsonConvert.DeserializeObject<User>(loginData);
                string Url = $"https://localhost:7276/api/CartDetail/Get-Cart-User?UserID={user.UserID}";
                var response = _client.GetStringAsync(Url).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    List<CartDetails> cartDetails = JsonConvert.DeserializeObject<List<CartDetails>>(response);
                    decimal totalAmount = cartDetails.Sum(cd => cd.Product.DiscountPrice * cd.Quantity);
                    decimal tax = totalAmount / 100 * 10;
                    decimal total = totalAmount + tax;
                    ViewBag.TotalAmount = totalAmount;
                    ViewBag.Tax = tax;
                    ViewBag.Total = total;
                    return View(cartDetails);
                }
                else
                {
                    ViewBag.TotalAmount = 0;
                    return View(new List<CartDetails>());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
		[HttpPost]
		public ActionResult UpdateCartDetail(Guid cartDetailID, int quantity)
		{
			try
			{
				// Lấy thông tin chi tiết giỏ hàng từ API
				var loginData = HttpContext.Session.GetString("user");
				if (loginData == null)
				{
					return RedirectToAction("Login", "User");
				}

				var user = JsonConvert.DeserializeObject<User>(loginData);
				var cartDetailUrl = $"https://localhost:7276/api/CartDetail/Get-Cart-User?UserID={user.UserID}";
				var cartDetailsResponse = _client.GetStringAsync(cartDetailUrl).Result;
				var cartDetails = JsonConvert.DeserializeObject<List<CartDetails>>(cartDetailsResponse);

				var cartDetailToUpdate = cartDetails.FirstOrDefault(cd => cd.CartDetailID == cartDetailID);
				if (cartDetailToUpdate == null)
				{
					return NotFound($"Cart detail with ID {cartDetailID} not found.");
				}

				// Cập nhật số lượng sản phẩm
				cartDetailToUpdate.Quantity = quantity;

				// Gửi dữ liệu cập nhật lên API
				var updateUrl = "https://localhost:7276/api/CartDetail/Update-CartDetail";
				var response = _client.PostAsJsonAsync(updateUrl, cartDetailToUpdate).Result;
				response.EnsureSuccessStatusCode();

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
