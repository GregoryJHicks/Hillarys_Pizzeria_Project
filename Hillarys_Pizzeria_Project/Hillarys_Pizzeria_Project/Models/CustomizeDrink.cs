namespace Hillarys_Pizzeria_Project.Models
{
    public static class CustomizeDrink
    {
        public static MenuItem selectedSize = new MenuItem();
        public static bool isSizeSelected = false;


        //Precondition:     Must be passed a valid MenuItem.
        //Postcondition:    Selects a new size for the custom drink.
        public static void SelectSize(MenuItem size)
        {
            isSizeSelected = true;
            selectedSize = size;
        }



        //Postcondition:    Resets the static CustomizeDrink.
        public static void ResetCustomizeDrink()
        {
            selectedSize = new MenuItem();
            isSizeSelected = false;
        }


        //Precondition:     Must be passed a valid MenuItem
        //Postcondition:    Calculates the cost of the new MenuItem based on the selected item and size and returns
        //                  the decimal cost;
        public static decimal CalculateCost(MenuItem selectedItem)
        {
            return selectedItem.price + selectedSize.price;
        }


        //Precondition:     Must be passed a valid MenuItem.
        //Postcondition:    Generates a description for the new MenuItem based on the selected item and size and
        //                  returns the stirng.
        public static string GenerateDescription(MenuItem selectedItem)
        {
            string description = selectedItem.description + "\nSize: " + selectedSize.description;
            return description;
        }


        //Precondition:     Size MUST be selected and it must be passed a valid MenuItem.
        //Postcondition:    Generates a new MenuItem based on selected item and size and adds the new MenuItem to
        //                  the static Cart class and resets the static CustomizeDrink for next use.
        public static void SendToCart(MenuItem selectedItem)
        {
            if (isSizeSelected) //Validates the precondition
            {
                //Start generating new MenuItem
                MenuItem newItem = new MenuItem();

                newItem.food_id = selectedItem.food_id;
                newItem.food_name = selectedItem.food_name;
                newItem.description = GenerateDescription(selectedItem);
                newItem.price = CalculateCost(selectedItem);
                //End generating new MenuItem

                Cart.AddToCart(newItem); //Adds item to static Cart class

                ResetCustomizeDrink(); //Resets the static CustomizeDrink
            }
            else //Nothing happens if precondition is not satisfied
            {
                return;
            }
        }
    }
}
