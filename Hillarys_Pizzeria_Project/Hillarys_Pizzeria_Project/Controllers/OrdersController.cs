using Microsoft.AspNetCore.Mvc;
using Hillarys_Pizzeria_Project.Models;

namespace Hillarys_Pizzeria_Project.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Receipt(int? targetID)
        {
            if (targetID != null)
            {
                if (targetID <= PlacedOrderList.count - 1)
                {
                    return View(PlacedOrderList.GetOrder((int)targetID));
                }
            }
            return View(PlacedOrderList.GetMostRecentOrder());
        }


        public IActionResult GenerateNewOrder(string firstName, string lastName, string phoneNumber,
            string streetAddress, string city, string state, string? deliveryInstructions, string cardNumber,
            string expirationDate, string CVV)
        {
            Order newOrder = new Order();

            newOrder.FirstName = firstName;
            newOrder.LastName = lastName;
            newOrder.PhoneNumber = phoneNumber;
            newOrder.StreetAddress = streetAddress;
            newOrder.City = city;
            newOrder.State = state;
            if (deliveryInstructions != null)
            {
                newOrder.DeliveryInstructions = deliveryInstructions;
            }
            newOrder.CardNumber = cardNumber;
            newOrder.CVV = CVV;
            newOrder.ExpirationDate = expirationDate;
            newOrder.DateCreated = DateTime.Now;
            newOrder.Content = new List<MenuItem>();
            newOrder.SubTotal = Cart.CalculateSubTotal();
            newOrder.Tax = Cart.CalculateTax();
            newOrder.Total = Cart.CalculateTotal();
            newOrder.OrderID = PlacedOrderList.GetNewOrderID();

            foreach (MenuItem item in Cart.contents)
            {
                newOrder.Content.Add(item);
            }

            Cart.ResetCart();
            PlacedOrderList.AddOrder(newOrder);

            return RedirectToAction("Receipt", PlacedOrderList.GetMostRecentOrder().OrderID);
        }
    }
}
