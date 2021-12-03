namespace Hillarys_Pizzeria_Project.Models
{
    public static class CustomizePizza
    {
        public static List<MenuItem> toppings = new List<MenuItem>();
        public static MenuItem selectedSize = new MenuItem();
        public static MenuItem selectedCrust = new MenuItem();

        public static int toppingsCount = 0;
        public static bool isSizeSelected = false;
        public static bool isCrustSelected = false;


        //Precondition:     The selected toppings must be less than four. Must be passed a valid MenuItem.
        //Postcondition:    Adds a topping to the list of selected toppings.
        public static void AddTopping(MenuItem item)
        {
            if (toppingsCount <= 4)
            {
                toppingsCount++;
                toppings.Add(item);
            }
            else
            {
                return;
            }
        }


        //Precondition:     Must be passed a valid MenuItem.
        //Postcondition:    Selects a size for the custom pizza and marks the bool value as true for selected size
        public static void SelectSize(MenuItem size)
        {
            isSizeSelected = true;
            selectedSize = size;
        }


        //Precondition:     Must be passed a valid MenuItem.
        //Postcondition:    Selects a crust for the custom pizza and marks the bool value as true for selected crust
        public static void SelectCrust(MenuItem crust)
        {
            isCrustSelected = true;
            selectedCrust = crust;
        }


        //Postcondition:    Resets the selected toppings to zero, setting the count to zero and the list to a new
        //                  list
        public static void ResetToppings()
        {
            toppingsCount = 0;
            toppings = new List<MenuItem>();
        }


        //Postcondition:    Resets the entire static CustomizePizza.
        public static void ResetCustomizePizza()
        {
            ResetToppings();
            isSizeSelected = false;
            isCrustSelected = false;
        }


        //Precondition:     Size and crust must be selected before use. Must be passed a valid MenuItem.
        //Postcondition:    Calculates the cost of the new MenuItem based on the selected item, toppings, size, and
        //                  crust and return the decimal cost;
        public static decimal CalculateCost(MenuItem selectedItem)
        {
            decimal cost = selectedItem.price;
            foreach (MenuItem item in toppings)
            {
                cost += item.price;
            }
            cost += selectedSize.price;
            cost += selectedCrust.price;
            return cost;
        }


        //Precondition:     Size and crust must be selected before use. Must be passed a valid MenuItem.
        //Postcondition:    Generates a description for the new MenuItem based on the selected item, toppings, size,
        //                  and crust options and returns the stirng.
        public static string GenerateDescription(MenuItem selectedItem)
        {
            string description = selectedItem.description;

            if (toppingsCount > 0)
            {
                description += "\nSelected Toppings:";
                foreach (MenuItem item in toppings)
                {
                    description += "\n" + Convert.ToString(item.description);
                }
            }

            description += "\nSize: " + selectedSize.description;
            description += "\nCrust: " + selectedCrust.description;

            return description;
        }


        //Precondition:     Size and crust MUST be selected, toppings are allowed to be empty or less than four.
        //                  Must be passed a valid MenuItem.
        //Postcondition:    Generates a new MenuItem based on selected item, toppings, size, and crust and adds
        //                  the new MenuItem to the static Cart class and resets the static CustomizePizza for
        //                  next use.
        public static void SendToCart(MenuItem selectedItem)
        {
            if (isSizeSelected && isCrustSelected) //Validates the precondition
            {
                //Start generating new MenuItem
                MenuItem newItem = new MenuItem();

                newItem.food_id = selectedItem.food_id;
                newItem.food_name = selectedItem.food_name;
                newItem.description = GenerateDescription(selectedItem);
                newItem.price = CalculateCost(selectedItem);
                //End generating new MenuItem

                Cart.AddToCart(newItem); //Adds item to static Cart class

                ResetCustomizePizza(); //Resets the static CustomizePizza
            }
            else //Nothing happens if precondition is not satisfied
            {
                return;
            }
        }
    }
}
