using Microsoft.AspNetCore.Mvc;

namespace Hillarys_Pizzeria_Project.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult CreateTemplate()
        {
            return View();
        }

        public IActionResult ProcessNewOrder()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
