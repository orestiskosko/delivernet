using DeliverNET.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeliverNET.Infrastructure.Account;
using DeliverNET.Models.AccountViewModels;
using Microsoft.Extensions.Logging;

namespace DeliverNET.Data
{
    public class Seeder
    {
        private readonly UserManager<DeliverNETUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<Seeder> _logger;
        private readonly DeliverNETClaimManager _claimManager;

        public Seeder(
            UserManager<DeliverNETUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DeliverNETClaimManager claimManager,
            ILogger<Seeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _claimManager = claimManager;
        }


        public async Task Seed()
        {
            string[] Roles = { "admin", "user" };

            foreach (var role in Roles)
            {
                var res = await _roleManager.RoleExistsAsync(role);
                if (!res) { await _roleManager.CreateAsync(new IdentityRole(role)); }
            }

            // TODO Add claims to those seeded madafaqas
            List<DeliverNETUser> testUsers = new List<DeliverNETUser>
            {
                new DeliverNETUser(){Email="admin@admins.com", UserName = "admin"},
                new DeliverNETUser(){Email="owner1@owners.com", UserName = "owner1"},
                new DeliverNETUser(){Email="owner2@owners.com", UserName = "owner2"},
                new DeliverNETUser(){Email="cashier1@cashiers.com", UserName = "cashier1"},
                new DeliverNETUser(){Email="cashier2@cashiers.com", UserName = "cashier2"},
                new DeliverNETUser(){Email="slave1@slaves.com", UserName = "slave1"},
                new DeliverNETUser(){Email="slave2@slaves.com", UserName = "slave2"},
                new DeliverNETUser(){Email="slave3@slaves.com", UserName = "slave3"}
            };

            DeliverNETUser user;
            foreach (var tu in testUsers)
            {
                user = await _userManager.FindByEmailAsync(tu.Email);
                if (user != null)
                    await _userManager.DeleteAsync(user);

                string password = tu.UserName;
                var result = await _userManager.CreateAsync(tu, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {tu.UserName} created succesfully.");

                    DeliverNETUser newUser = await _userManager.FindByEmailAsync(tu.Email);

                    if (password.Contains("admin"))
                        await _userManager.AddToRoleAsync(newUser, "admin");

                    await _userManager.AddToRoleAsync(newUser, "user");

                    if (password.Contains("owner"))
                        await _claimManager.AddClaim(newUser, JobTypes.Businessman);
                    else if (password.Contains("slave"))
                        await _claimManager.AddClaim(newUser, JobTypes.Individual);
                }
                else
                {
                    _logger.LogInformation($"User {tu.UserName} was not created.");
                }
            }

        }
    }
}

