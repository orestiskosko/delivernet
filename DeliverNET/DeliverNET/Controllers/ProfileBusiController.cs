﻿using System;
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
    }
}