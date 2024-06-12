using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Bill")]
    public class BillController : Controller
    {
        HttpClient _client = new HttpClient();
        [Route("Index")]
        public ActionResult Index()
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-All-Bill";
            var response = _client.GetStringAsync(Url).Result;
            List<Bill> categories = JsonConvert.DeserializeObject<List<Bill>>(response);
            return View(categories);
        }
        [HttpGet("Details/{Id}")]
        public ActionResult Details(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-ID-Bill?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            return View(bill);
        }
        [HttpGet("BuyAgain/{Id}")]
        public ActionResult BuyAgain(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-ID-Bill?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            return View(bill);
        }
        [HttpGet("CancelPayment/{Id}")]
        public ActionResult CancelPayment(Guid Id)
        {
            string Url = $@"https://localhost:7276/api/Bill/Get-ID-Bill?id={Id}";
            var response = _client.GetStringAsync(Url).Result;
            Bill bill = JsonConvert.DeserializeObject<Bill>(response);
            return View(bill);
        }
    }
}
