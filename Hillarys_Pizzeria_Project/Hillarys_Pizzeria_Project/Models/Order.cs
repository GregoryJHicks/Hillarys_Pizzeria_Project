namespace Hillarys_Pizzeria_Project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? DeliveryInstructions { get; set; }
        public List<MenuItem> Content { get; set; }
        public int CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public int CVV { get; set; }
        public DateTime dateCreated { get; set; }

    }
}
