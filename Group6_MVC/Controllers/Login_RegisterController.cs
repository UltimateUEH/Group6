using Group6_MVC.Models;
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
        }

        public IActionResult Index()
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
