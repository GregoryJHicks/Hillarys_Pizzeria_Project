namespace Hillarys_Pizzeria_Project.Models
{
    public static class Cart
    {
        public static List<MenuItem> contents = new List<MenuItem>();
        public const decimal GASalesTax = 0.06M;

        public static void AddToCart(MenuItem item)
        {
            contents.Add(item);
        }

        public static void ResetCart()
        {
            contents.Clear();
        }

        public static bool IsNotEmpty()
        {
            return contents.Count != 0;
        }

        public static decimal CalculateSubTotal()
        {
            decimal total = 0;
            foreach (var item in contents)
            {
                total += item.price;
            }
            return total;
        }

        public static decimal CalculateTax()
        {
            return Math.Round(CalculateSubTotal() * GASalesTax, 2);
        }

        public static decimal CalculateTotal()
        {
            return Math.Round(CalculateSubTotal() + CalculateTax(), 2);
        }
    }
}
