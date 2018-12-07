using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeliverNET.Controllers
{
    public class ProfileBusiController : Controller
    {
        public IActionResult IndexBusi()
        {
            return View();
        }

        public IActionResult DashboardBusi()
        {
            return View();
        }

        public IActionResult SettingsBusi()
        {
            return View();
        }

        public IActionResult AppFormBusi()
        {
            return View();
        }
    }
}