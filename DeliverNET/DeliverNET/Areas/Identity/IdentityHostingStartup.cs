using System;
using DeliverNET.Areas.Identity.Data;
using DeliverNET.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DeliverNET.Areas.Identity.IdentityHostingStartup))]
namespace DeliverNET.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DeliverNETContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DeliverNETContextConnection")));

                services.AddDefaultIdentity<DeliverNETUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DeliverNETContext>();
            });
        }
    }
}