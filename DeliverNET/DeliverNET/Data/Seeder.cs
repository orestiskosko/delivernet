using DeliverNET.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Data
{
    public class Seeder
    {
        public async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<DeliverNETUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] Roles = { "admin", "user" };

            foreach (var role in Roles)
            {
                var res = await roleManager.RoleExistsAsync(role);
                if (!res) { await roleManager.CreateAsync(new IdentityRole(role)); }
            }


            List<DeliverNETUser> testUsers = new List<DeliverNETUser>
            {
                new DeliverNETUser(){Email="admin@admins.com", UserName = "admin@admins.com"},
                new DeliverNETUser(){Email="owner1@owners.com", UserName = "owner1@owners.com"},
                new DeliverNETUser(){Email="owner2@owners.com", UserName = "owner2@owners.com"},
                new DeliverNETUser(){Email="cashier1@cashiers.com", UserName = "cashier1@cashiers.com"},
                new DeliverNETUser(){Email="cashier2@cashiers.com", UserName = "cashier2@cashiers.com"},
                new DeliverNETUser(){Email="slave1@slaves.com", UserName = "slave1@slaves.com"},
                new DeliverNETUser(){Email="slave2@slaves.com", UserName = "slave2@slaves.com"},
                new DeliverNETUser(){Email="slave3@slaves.com", UserName = "slave3@slaves.com"}
            };

            DeliverNETUser user;
            foreach (var tu in testUsers)
            {
                user = await userManager.FindByEmailAsync(tu.Email);
                if (user == null)
                {
                    user = new DeliverNETUser()
                    {
                        Email = tu.Email,
                        UserName = tu.Email
                    };
                    string password = user.Email.Split('@').First();
                    await userManager.CreateAsync(user, password);

                    if (password.Contains("admin"))
                        await userManager.AddToRoleAsync(user, "admin");

                    await userManager.AddToRoleAsync(user, "user");
                }
            }

        }
    }
}

