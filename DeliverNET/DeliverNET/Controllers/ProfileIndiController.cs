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
    }
}