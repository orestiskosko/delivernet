using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeliverNET.Data.VMs;

namespace DeliverNET.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult SendToLoginIdentity(UserLoginVM userLogin)
        {
            return RedirectToPage("/Areas/Identity/Pages/Account/Login");
        }
    }
}