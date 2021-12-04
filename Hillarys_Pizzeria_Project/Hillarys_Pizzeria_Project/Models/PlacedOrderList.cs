namespace Hillarys_Pizzeria_Project.Models
{
    public static class PlacedOrderList
    {
        public static List<Order> contents = new List<Order>();
        public static int count = 0;

        public static void AddOrder(Order order)
        {
            contents.Add(order);
            count++;
        }

        public static Order GetMostRecentOrder()
        {
            Order result = contents[count - 1];
            return result;
        }

        public static Order GetOrder(int id)
        {
            Order target = new Order();
            if (id < contents.Count)
            {
                target = contents[id];
            }
            return target;
        }

        public static int GetNewOrderID()
        {
            return count;
        }
    }
}
