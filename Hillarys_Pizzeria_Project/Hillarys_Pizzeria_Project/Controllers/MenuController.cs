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

        public IActionResult CustomizeDrinkView(int selectedItemID)
        {
            List<MenuItem> drinks = LoadJson(MenuType.Drinks);
            List<MenuItem> beverageSizes = LoadJson(MenuType.BeverageSizes);
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in drinks)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            beverageSizes.Add(selectedItem);

            return View(beverageSizes);
        }

        public IActionResult CustomizePizzaView(int selectedItemID)
        {
            List<MenuItem> pizzas = LoadJson(MenuType.Pizza);
            List<MenuItem> pizzaCustomization = LoadJson(MenuType.PizzaCustomization);
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in pizzas)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            pizzaCustomization.Add(selectedItem);

            return View(pizzaCustomization);
        }

        public IActionResult CustomizeItem(MenuType type, int selectedItemID)
        {
            List<MenuItem> list, menu;
            switch (type)
            {
                case MenuType.PizzaCustomization:
                    list = LoadJson(MenuType.PizzaCustomization);
                    menu = LoadJson(MenuType.Pizza);
                    break;
                default: //Drinks options
                    list = LoadJson(MenuType.BeverageSizes);
                    menu = LoadJson(MenuType.Drinks);
                    break;
            }

            foreach (MenuItem item in menu)
            {
                if (item.food_id == selectedItemID)
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

        public IActionResult ResetToppingSelecion(int selectedItemID)
        {
            List<MenuItem> pizza = LoadJson(MenuType.Pizza);
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in pizza)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            CustomizePizza.ResetToppings();
            return RedirectToAction("CustomizePizzaView", new { selectedItemID = selectedItem.food_id });
        }

        public IActionResult AddTopping(int toppingID, int selectedItemID)
        {
            List<MenuItem> pizzaCustomization = LoadJson(MenuType.PizzaCustomization);
            List<MenuItem> pizza = LoadJson(MenuType.Pizza);
            MenuItem topping = new MenuItem();
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in pizzaCustomization)
            {
                if (item.food_id == toppingID)
                {
                    topping = item;
                }
            }
            foreach (MenuItem item in pizza)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            CustomizePizza.AddTopping(topping);
            return RedirectToAction("CustomizePizzaView", new { selectedItemID = selectedItem.food_id });
        }

        public IActionResult SetPizzaSize(int sizeID, int selectedItemID)
        {
            List<MenuItem> pizzaCustomization = LoadJson(MenuType.PizzaCustomization);
            List<MenuItem> pizza = LoadJson(MenuType.Pizza);
            MenuItem size = new MenuItem();
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in pizzaCustomization)
            {
                if (item.food_id == sizeID)
                {
                    size = item;
                }
            }
            foreach (MenuItem item in pizza)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            CustomizePizza.SelectSize(size);
            return RedirectToAction("CustomizePizzaView", new { selectedItemID = selectedItem.food_id });
        }

        public IActionResult SetPizzaCrust(int crustID, int selectedItemID)
        {
            List<MenuItem> pizzaCustomization = LoadJson(MenuType.PizzaCustomization);
            List<MenuItem> pizza = LoadJson(MenuType.Pizza);
            MenuItem crust = new MenuItem();
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in pizzaCustomization)
            {
                if (item.food_id == crustID)
                {
                    crust = item;
                }
            }
            foreach (MenuItem item in pizza)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            CustomizePizza.SelectCrust(crust);
            return RedirectToAction("CustomizePizzaView", new { selectedItemID = selectedItem.food_id });
        }

        public IActionResult SetDrinkSize(int sizeID, int selectedItemID)
        {
            List<MenuItem> sizeOptions = LoadJson(MenuType.BeverageSizes);
            List<MenuItem> drinks = LoadJson(MenuType.Drinks);
            MenuItem size = new MenuItem();
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in sizeOptions)
            {
                if (item.food_id == sizeID)
                {
                    size = item;
                }
            }
            foreach (MenuItem item in drinks)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            CustomizeDrink.SelectSize(size);
            return RedirectToAction("CustomizeDrinkView", new { selectedItemID = selectedItem.food_id });
        }


        public IActionResult AddToCart(int selectedItemID, MenuType menu)
        {
            List<MenuItem> menuItems = LoadJson(menu);
            MenuItem selectedItem = new MenuItem();

            foreach (MenuItem item in menuItems)
            {
                if (item.food_id == selectedItemID)
                {
                    selectedItem = item;
                }
            }

            switch (menu)
            {
                case MenuType.Drinks:
                    CustomizeDrink.SendToCart(selectedItem);
                    return RedirectToAction("Drinks");

                case MenuType.Pizza:
                    CustomizePizza.SendToCart(selectedItem);
                    return RedirectToAction("Pizzas");

                case MenuType.Sides:
                    Cart.AddToCart(selectedItem);
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
