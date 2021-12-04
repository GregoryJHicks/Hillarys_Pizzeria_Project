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

        public IActionResult RemoveItem(int targetID)
        {
            MenuItem targetItem = new MenuItem();
            MenuType menu;

            if (100 <= targetID & targetID <= 199)
            {
                menu = MenuType.Pizza;
            }
            else if (200 <= targetID & targetID <= 299)
            {
                menu = MenuType.Sides;
            }
            else
            {
                menu = MenuType.Drinks;
            }
            return RedirectToAction("Index");
        }
    }
}
