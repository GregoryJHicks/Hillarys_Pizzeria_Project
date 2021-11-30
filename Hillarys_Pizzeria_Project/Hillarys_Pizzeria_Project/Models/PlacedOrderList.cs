namespace Hillarys_Pizzeria_Project.Models
{
    public static class PlacedOrderList
    {
        public static List<Order> contents = new List<Order>();

        public static void AddOrder(Order order)
        {
            contents.Add(order);
        }

        public static int MostRecentID()
        {
            int result = contents.Count() - 1;
            return result;
        }

        public static Order GetOrder(int id)
        {
            Order target = new Order();
            if (id < contents.Count)
            {
                target = contents[id - 1];
            }
            return target;
        }
    }
}
