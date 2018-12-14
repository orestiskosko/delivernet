using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Comms.Hubs;
using DeliverNET.Data;
using DeliverNET.Managers;
using DeliverNET.Managers.Interfaces;
using DeliverNET.Models.AccountViewModels;
using DeliverNET.Models.ProfileBusiViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<DeliverNETUser> _userManager;
        private readonly IMasterManager _masterManager;

        public ProfileBusiController(
            UserManager<DeliverNETUser> userManager,
            IMasterManager masterManager
            )
        {
            _userManager = userManager;
            _masterManager = masterManager;
        }

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

        [HttpGet]
        public IActionResult VerifyFormBusi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyFormBusi(VerifyFormBusiViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                BusinessOwner owner = _masterManager.GetBusinessOwnerManager().Get(userId);
                int businessId = _masterManager.GetBusinessManager().Get(owner.BusinessId).Id;
                _masterManager.GetBusinessManager().SetTitle(businessId, model.Title);
                _masterManager.GetBusinessManager().SetAddress(businessId, model.Address);
                _masterManager.GetBusinessManager().SetPhoneNumber(businessId, model.PhoneNumber);
                _masterManager.GetBusinessManager().SetCredentials(businessId, model.Credentials);
                return RedirectToAction("IndexBusi");
            }
            return View(model);
        }
    }
}