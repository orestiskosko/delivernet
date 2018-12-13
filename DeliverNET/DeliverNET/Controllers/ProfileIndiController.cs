﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;
using DeliverNET.Managers.Interfaces;
using DeliverNET.Models;
using DeliverNET.Models.ProfileIndiViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliverNET.Controllers
{
    public class ProfileIndiController : Controller
    {
        // Dummy order data
        private static Models.ProfileIndividual.IndividualIndexModel Order_Dummy = new Models.ProfileIndividual.IndividualIndexModel
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


        private IDelivererManager _delivererManager;
        private UserManager<DeliverNETUser> _userManager;

        public ProfileIndiController(
            UserManager<DeliverNETUser> userManager,
            IDelivererManager delivererManager
            )
        {
            _delivererManager = delivererManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult IndexIndi()
        {
            return View(Order_Dummy);
        }

        [HttpGet]
        public IActionResult DashboardIndi()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SettingsIndi()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VerifyFormIndi()
        {
            return View();
        }

        public async Task<IActionResult> VerifyIdiForm(VerifyFormIndiViewModel model)
        {
            if (ModelState.IsValid)
            {
                DeliverNETUser user =  await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                _delivererManager.SetOperatingRegion(user.Id, $"{model.OperationalCity} / {model.OperationalRegion}");
                

            }
            return View(model);
        }
    }
}