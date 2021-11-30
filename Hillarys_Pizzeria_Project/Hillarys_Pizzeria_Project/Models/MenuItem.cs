using System.ComponentModel.DataAnnotations;

namespace Hillarys_Pizzeria_Project.Models
{
    public class MenuItem
    {
        public int food_id { get; set; }
        public string food_name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
    }
}
