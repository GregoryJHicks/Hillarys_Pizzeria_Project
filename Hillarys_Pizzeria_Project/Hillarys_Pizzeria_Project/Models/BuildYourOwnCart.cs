namespace Hillarys_Pizzeria_Project.Models
{
    public static class BuildYourOwnCart
    {
        public static List<MenuItem> contents = new List<MenuItem>();

        public static void AddOption(MenuItem item)
        {
            contents.Add(item);
        }

        public static void ResetCart()
        {
            contents.Clear();
        }

        public static void SendToCart()
        {
            MenuItem item = new MenuItem();
            item.food_id = 999;
            item.food_name = "Custom Pizza";
            item.price = CalculateTotal();
            item.description = CreateDescription();
            Cart.AddToCart(item);
            ResetCart();
        }

        public static decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (MenuItem item in contents)
            {
                total += item.price;
            }

            return total;
        }

        public static string CreateDescription()
        {
            string description = "";

            foreach(MenuItem item in contents)
            {
                description += "/n" + item.food_name;
            }

            return description;
        }

        public static bool IsNotEmpty()
        {
            return contents.Count() > 0;
        }
    }
}
