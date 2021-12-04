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

        public IActionResult Receipt()//Receipt for just generated order
        {
            return View(PlacedOrderList.contents[PlacedOrderList.MostRecentID()]);
        }

        public IActionResult Receipt(int targetID)//Select an order to get its receipt
        {
            if (targetID <= PlacedOrderList.contents.Count() - 1)
            {
                return View(PlacedOrderList.GetOrder(targetID));
            }
            else
            {
                return RedirectToAction("Receipt");
            }
        }

        public IActionResult GenerateNewOrder()
        {
            return RedirectToAction("Receipt", PlacedOrderList.MostRecentID());
        }

        public IActionResult RemoveItem(int targetID)
        {
            return RedirectToAction("Index");
        }
    }
}
