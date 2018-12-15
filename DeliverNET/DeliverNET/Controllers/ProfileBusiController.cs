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
        [HttpGet]
        public IActionResult IndexBusi()
        {
            return View();
        }

        // TODO Authorize only business owners
        [HttpGet]
        public IActionResult DashboardBusi()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SettingsBusi()
        {
            return View();
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