using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliverNET.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            // TODO Add user validation logic
            return View();
        }

        public ActionResult Logout()
        {
            // TODO Add singoff
            return RedirectToAction("Login","Auth");
        }
    }
}