using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeliverNET.Controllers
{
    public class ProfileIndiController : Controller
    {
        public IActionResult IndexIndi()
        {
            return View();
        }

        public IActionResult DashboardIndi()
        {
            return View();
        }

        public IActionResult SettingsIndi()
        {
            return View();
        }

        public IActionResult AppFormIndi()
        {
            return View();
        }
    }
}