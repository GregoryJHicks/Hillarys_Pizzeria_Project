using Hillarys_Pizzeria_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hillarys_Pizzeria_Project.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pizzas()
        {
            List<MenuItem> menu = LoadJson(MenuType.Pizza);
            return View(menu);
        }

        public IActionResult Sides()
        {
            List<MenuItem> menu = LoadJson(MenuType.Sides);
            return View(menu);
        }

        public IActionResult Drinks()
        {
            List<MenuItem> menu = LoadJson(MenuType.Drinks);
            return View(menu);
        }

        public IActionResult BuildYourOwn()
        {
            List<MenuItem> menu = LoadJson(MenuType.Custom);
            return View(menu);
        }

        public List<MenuItem> LoadJson(MenuType type)
        {
            List<MenuItem> menu = new List<MenuItem>();
            StreamReader stream;
            string file = "";
            
            switch (type)
            {
                case MenuType.Pizza:
                    file = "PizzaMenu.json";
                    break;
                case MenuType.Sides:
                    file = "SidesMenu.json";
                    break;
                case MenuType.Drinks:
                    file = "DrinksMenu.json";
                    break;
                case MenuType.Custom:
                    file = "CustomizationOptions.json";
                    break;
            }
            using (stream = new StreamReader(file))
            {
                string json = stream.ReadToEnd();
                menu = JsonConvert.DeserializeObject<List<MenuItem>>(json);
            }
            return menu;
        }

        public IActionResult AddToCart(int food_id, MenuType menu)
        {
            List<MenuItem> tempMenu = LoadJson(menu);
            MenuItem target = new MenuItem();

            foreach (MenuItem item in tempMenu)
            {
                if (item.food_id == food_id)
                {
                    target = item;
                }
            }

            Cart.AddToCart(target);

            switch(menu)
            {
                case MenuType.Pizza:
                    return RedirectToAction("Pizzas");
                case MenuType.Sides:
                    return RedirectToAction("Sides");
                case MenuType.Drinks:
                    return RedirectToAction("Drinks");
                case MenuType.Custom:
                    return RedirectToAction("Pizzas");
                default:
                    return RedirectToAction("Index");
            }
        }


        public IActionResult AddToBuildYourOwnCart(int targetFoodID)
        {
            List<MenuItem> tempMenu = LoadJson(MenuType.Custom);

            foreach (MenuItem item in tempMenu)
            {
                if (item.food_id == targetFoodID)
                {
                    BuildYourOwnCart.AddOption(item);
                }
            }

            return RedirectToAction("BuildYourOwn");
        }

        public IActionResult CreateBuildYourOwn()
        {
            BuildYourOwnCart.SendToCart();
            return RedirectToAction("Index");
        }
    }

    public enum MenuType
    {
        Pizza = 0,
        Sides = 1,
        Drinks = 2,
        Custom = 3
    }

    //Format for User Defined Exceptions found at:
    //https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
    public class NotValidMenuID:Exception
    {
        public NotValidMenuID(string message)
            :base(message)
        {
        }
    }
}
