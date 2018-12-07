using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DeliverNET.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DeliverNET.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;

namespace DeliverNET.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<DeliverNETUser> _userManager;
        private readonly SignInManager<DeliverNETUser> _signInManager;
        private readonly ILogger _logger;

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountController(
            UserManager<DeliverNETUser> userManager,
            SignInManager<DeliverNETUser> signInManager,
            ILogger<AccountController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // TODO Change lockout on failure when implemented
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {0} logged in.", model.Email);

                    // TODO Decide if deliverer or business and redirect acoordingly!
                    //var user = await _userManager.FindByEmailAsync(model.Email);
                    //var userClaims = await _userManager.GetClaimsAsync(user);
                    //bool IsDeliverer = userClaims.Where(x => x.Value == "DelivererOnly").;
                    return RedirectToAction("Index", "Home");
                }


            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index","Home");
        }

    }
}