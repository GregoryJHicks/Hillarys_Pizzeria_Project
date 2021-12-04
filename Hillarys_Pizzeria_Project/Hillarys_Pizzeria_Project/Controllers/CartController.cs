using Microsoft.AspNetCore.Mvc;
using Hillarys_Pizzeria_Project.Models;

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
            if (Cart.IsNotEmpty())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateTemplate()
        {
            return View();
        }

        public IActionResult ProcessNewOrder(string firstName, string lastName, int phoneNumber, string streetAddress, string city, string state, string? deliveryInstructions, int cardNumber, DateTime expirationDate, int CVV)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
