using Microsoft.AspNetCore.Mvc;

namespace Group6_MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
