using Hillarys_Pizzeria_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hillarys_Pizzeria_Project.Controllers
{
    public class MenuController : Controller
    {


        //Postcondition:    Generates the MenuController's Index view
        public IActionResult Index()
        {
            return View();
        }


        //Postcondition:    Generates the MenuController's Pizzas view and passes it a List<MenuItem> model
        //                  consisting of the pizza menu
        public IActionResult Pizzas()
        {
            List<MenuItem> menu = LoadJson(MenuType.Pizza);
            return View(menu);
        }


        //Postcondition:    Generates the MenuController's Sides view and passes it a List<MenuItem> model
        //                  consisting of the sides menu
        public IActionResult Sides()
        {
            List<MenuItem> menu = LoadJson(MenuType.Sides);
            return View(menu);
        }


        //Postcondition:    Generates the MenuController's Drinks view and passes it a List<MenuItem> model
        //                  consisting of the drinks menu
        public IActionResult Drinks()
        {
            List<MenuItem> menu = LoadJson(MenuType.Drinks);
            return View(menu);
        }


        //Precondition:     Must be passed a valid MenuItem's id
        //Postcondition:    Generates the MenuController's CustomizeDrinkView view and passes it a List<MenuItem>
        //                  model consisting of the list of available beverage sizes, and the selected item added
        //                  as the last item to the list
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


        //Precondition:     Must be passed a valid MenuItem's id
        //Postcondition:    Generates the MenuController's CustomizePizzaView view and passes it a List<MenuItem>
        //                  model consisting of the list of available pizza customization options, and the
        //                  selected item added as the last item to the list
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


        //Precondition:     Must be passed a valid MenuType variable indicating the desired file
        //Postcondition:    Resturns a List<MenuItem> containing the contents from the json file indicated by
        //                  the given parameter
        public List<MenuItem> LoadJson(MenuType type)
        {
            List<MenuItem> tempList = new List<MenuItem>();
            List<MenuItem> menu = new List<MenuItem>();
            StreamReader stream;
            string file;

            switch (type)
            {
                case MenuType.Pizza:
                case MenuType.Sides:
                case MenuType.Drinks:
                    file = "FullMenu.json";
                    break;
                default:
                    file = "Customization.json";
                    break;
            }
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


        //Precondition:     Must be passed a valid int variable, indicating the selected item
        //Postcondition:    Resets the toppings held in the static class CustomizePizza and redirects the program
        //                  to the CustomizePizzaView method with the selecteditem's id as the parameter
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


        //Precondition:     Must be passed a valid int variable indicating the desired topping, and must be passed
        //                  a valid int variable indicating the desired selected item
        //Postcondition:    Adds the topping indicated by the first parameter to the static CustomizePizza and 
        //                  redirects to the CustomizePizzaView method with the selected item's id as the parameter
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


        //Precondition:     Must be passed a valid int variable indicating the desired size, and must be passed an
        //                  int variable indicating the desired selected item
        //Postcondition:    Sets the static CustomizePizza's size to the size indicated by the first parameter and
        //                  redirects to the CustomizePizzaView method with the selected item's id as the parameter
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


        //Precondition:     Must be passed a valid int variable indicating the desired crust, and must be passed an
        //                  int variable indicating the desired selected item
        //Postcondition:    Sets the static CustomizePizza's crust to the crust indicated by the first parameter and
        //                  redirects to the CustomizePizzaView method with the selected item's id as the parameter
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


        //Precondition:     Must be passed a valid int variable indicating the desired size, and must be passed a
        //                  valid int variable indicating the desired selected item
        //Postcondition:    Set's the static CustomizeDrink's size to the size indicated by the first parameter and
        //                  redirects to the CustomizeDrinkView method with the selected item's id as the parameter
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


        //Precondition:     Must be passed a valid int indicating the desired selected item and must be passed a
        //                  valid MenuType indicating the desired menu
        //Postcondition:    Uses the appropriate methods to add the correct item indicated by both parameters to the
        //                  static cart class and resets any necessary static classes
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

        public IActionResult RemoveFromCart(int targetID)
        {
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

            List<MenuItem> list = LoadJson(menu);
            MenuItem targetItem = new MenuItem();

            foreach (MenuItem item in list)
            {
                if (item.food_id == targetID)
                {
                    targetItem = item;
                }
            }
            try
            {
                Cart.RemoveFromCart(targetItem);
            }
            catch
            {
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");

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
