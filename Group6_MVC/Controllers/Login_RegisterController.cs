using Group6_MVC.Models;
using Group6_WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Group6_MVC.Controllers
{
    public class Login_RegisterController : Controller
    {
        private readonly ILogger<Login_RegisterController> _logger;

        public Login_RegisterController(ILogger<Login_RegisterController> logger)
        {
            _logger = logger;
            // check new u see
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
