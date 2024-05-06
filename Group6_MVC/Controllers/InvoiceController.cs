using Microsoft.AspNetCore.Mvc;

namespace Group6_MVC.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
