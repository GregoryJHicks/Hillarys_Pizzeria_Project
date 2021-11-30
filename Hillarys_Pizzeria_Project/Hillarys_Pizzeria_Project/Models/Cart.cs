namespace Hillarys_Pizzeria_Project.Models
{
    public static class Cart
    {
        public static List<MenuItem> contents = new List<MenuItem>();

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
    }
}
