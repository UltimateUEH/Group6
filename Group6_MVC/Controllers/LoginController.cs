using Microsoft.AspNetCore.Mvc;
using Group6_MVC.Models;
using Group6_WebApi.Models;
using System.Diagnostics;

namespace Group6_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly HttpClient _httpClient;

        public LoginController(ILogger<LoginController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            // check new u see
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            // Thiết lập BaseAddress
            _httpClient.BaseAddress = new Uri("https://localhost:7283/api/"); // change thanh cai port cua moi nguoi

            // Gửi yêu cầu với URI tương đối
            var response = await _httpClient.GetAsync("Invoice");

            if (response.IsSuccessStatusCode)
            {
                var invoices = await response.Content.ReadFromJsonAsync<List<Invoice>>();

                return View(invoices);
            }
            else
            {
                return View(new List<Invoice>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
