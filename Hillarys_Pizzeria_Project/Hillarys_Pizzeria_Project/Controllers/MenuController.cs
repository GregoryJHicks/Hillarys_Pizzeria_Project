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

        public IActionResult CustomizeItem(MenuType type, int ID)
        {
            List<MenuItem> list, menu;
            switch (type)
            {
                case MenuType.PizzaCustomization:
                    list = LoadJson(MenuType.PizzaCustomization);
                    menu = LoadJson(MenuType.Pizza);
                    break;
                default: //Sides options
                    list = LoadJson(MenuType.BeverageSizes);
                    menu = LoadJson(MenuType.Drinks);
                    break;
            }

            foreach (MenuItem item in menu)
            {
                if (item.food_id == ID)
                {
                    list.Add(item);
                    break;
                }
            }

            return View(list);
        }

        public List<MenuItem> LoadJson(MenuType type)
        {
            List<MenuItem> tempList = new List<MenuItem>();
            List<MenuItem> menu = new List<MenuItem>();
            StreamReader stream;
            string file;

            if (type == MenuType.Pizza)
            {
                file = "FullMenu.json";
            }
            else if (type == MenuType.Sides)
            {
                file = "FullMenu.json";
            }
            else if (type == MenuType.Drinks)
            {
                file = "FullMenu.json";
            }
            else
            {
                file = "Customization.json";
            }

            using (stream = new StreamReader(file))
            {
                string json = stream.ReadToEnd();
                tempList = JsonConvert.DeserializeObject<List<MenuItem>>(json);
            }

            switch (type)
            {
                case MenuType.PizzaCustomization:
                    foreach (MenuItem item in tempList)
                    {
                        if (100 <= item.food_id & item.food_id <= 399)
                        {
                            menu.Add(item);
                        }
                    }
                    break;
                case MenuType.Pizza:
                    foreach(MenuItem item in tempList)
                    {
                        if (100 <= item.food_id & item.food_id <= 199)
                        {
                            menu.Add(item);
                        }
                    }
                    break;
                case MenuType.Sides:
                    foreach (MenuItem item in tempList)
                    {
                        if (200 <= item.food_id & item.food_id <= 299)
                        {
                            menu.Add(item);
                        }
                    }
                    break;
                case MenuType.Drinks:
                    foreach (MenuItem item in tempList)
                    {
                        if (300 <= item.food_id & item.food_id <= 399)
                        {
                            menu.Add(item);
                        }
                    }
                    break;
                case MenuType.BeverageSizes:
                    foreach (MenuItem item in tempList)
                    {
                        if (400 <= item.food_id & item.food_id <= 499)
                        {
                            menu.Add(item);
                        }
                    }
                    break;
                default:
                    menu = tempList;
                    break;
            }

            return menu;
        }

        public IActionResult ResetToppingSelecion(MenuItem selectedItem)
        {
            CustomizePizza.ResetToppings();
            return RedirectToAction("CustomizeItem", new { type = MenuType.PizzaCustomization, ID = selectedItem.food_id });
        }

        public IActionResult AddPizzaTopping(MenuItem topping, MenuItem selectedItem)
        {
            CustomizePizza.AddTopping(topping);
            return RedirectToAction("CustomizeItem", new { type = MenuType.PizzaCustomization, ID = selectedItem.food_id });
        }

        public IActionResult SetPizzaSize(MenuItem size, MenuItem selectedItem)
        {
            CustomizePizza.SelectSize(size);
            return RedirectToAction("CustomizeItem", new { type = MenuType.PizzaCustomization, ID = selectedItem.food_id });
        }

        public IActionResult SetPizzaCrust(MenuItem crust, MenuItem selectedItem)
        {
            CustomizePizza.SelectCrust(crust);
            return RedirectToAction("CustomizeItem", new { type = MenuType.PizzaCustomization, ID = selectedItem.food_id });
        }

        public IActionResult SetDrinkSize(MenuItem size, MenuItem selectedItem)
        {
            CustomizeDrink.SelectSize(size);
            return RedirectToAction("CustomizeItem", new { type = MenuType.BeverageSizes, ID = selectedItem.food_id });
        }


        public IActionResult AddToCart(MenuItem item, MenuType menu)
        {
             switch (menu)
            {
                case MenuType.BeverageSizes:
                    CustomizeDrink.SendToCart(item);
                    return RedirectToAction("Drinks");

                case MenuType.PizzaCustomization:
                    CustomizePizza.SendToCart(item);
                    return RedirectToAction("Pizzas");

                case MenuType.Sides:
                    Cart.AddToCart(item);
                    return RedirectToAction("Sides");

                default:
                    Cart.AddToCart(new MenuItem());
                    return RedirectToAction("Index");
            }
        }
    }

    public enum MenuType
    {
        Pizza = 0,
        Sides = 1,
        Drinks = 2,
        PizzaCustomization = 3,
        BeverageSizes = 4
    }
}
