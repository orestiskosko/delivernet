using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Comms.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DeliverNET.Controllers
{
    public class ProfileBusiController : Controller
    {
        private static Models.ProfileDummies.BusiIndixDummyModel Order_Dummy = new Models.ProfileDummies.BusiIndixDummyModel
        {
            IsWorking = false,
            IsVerified = false,
            DelivererUsername = "Pantazakos",
            NumberOfOrders = 3,
            RestaurantName = "Ravaisis",
            RestaurantAddress = "Arxiloxou 0",
            OrderID = 1234,
            FirstName = "Dimosthenis",
            LastName = "Pasparakis",
            Address = "Kapou 32",
            FloorNo = 2,
            DoorName = "AFDEmp",
            PaymentTypeId = 1,
            State = "Picked",
            DistanceToShop = 1.5F,
            ETA = 7,
            Price = 10.4F,
            CountWatch = 4.03F,
            Datetime = DateTime.Now
        };

        // TODO Authorize only business cashiers
        public IActionResult IndexBusi()
        {
            return View(Order_Dummy);
        }

        // TODO Authorize only business owners
        public IActionResult DashboardBusi()
        {
            return View();
        }


        public IActionResult SettingsBusi()
        {
            return View(Order_Dummy);
        }


        public IActionResult VerifyFormBusi()
        {
            return View();
        }
    }
}