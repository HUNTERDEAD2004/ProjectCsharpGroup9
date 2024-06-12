using Microsoft.AspNetCore.Mvc;
using ProjectCsharpGroup9.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectCsharpGroup9.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly string _apiBaseUrl = "https://localhost:7276/api/Product";
        HttpClient _httpClient;
        
        public ProductController()
        {
            _httpClient = new HttpClient();
        }
        

        
    }
}
