using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverNET.Controllers
{
    public class ProfileIndiController : Controller
    {
        private static Models.ProfileDummies.IndiIndixDummyModel Order_Dummy = new Models.ProfileDummies.IndiIndixDummyModel
        {
            IsWorking = false,
            Username = "Pantazakos",
            ActiveOrders = 3,
            RestaurantName = "Ravaisis",
            RestaurantAddress = "Arxiloxou 0",
            OrderID = 1234,
            FirstName = "Dimosthenis",
            LastName = "Pasparakis",
            Address = "Kapou 32",
            FloorNo = 2,
            DoorName = "AFDEmp",
            PaymentTypeId = 0,
            State = "Picked",
            DistanceToShop = 1.5F,
            ETA = 7,
            Price = 10.4F,
            Stopwatch = 4.03F
        };

        public IActionResult IndexIndi()
        {

            return View(Order_Dummy);
        }

        public IActionResult DashboardIndi()
        {
            return View();
        }

        public IActionResult SettingsIndi()
        {
            return View();
        }

        public IActionResult VerifyFormIndi()
        {
            return View();
        }
    }
}