using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeliverNET.Controllers
{
    public class DashboardIndiController : Controller
    {
        // Dummy slave dashboard data
        private static Models.ProfileDummies.IndiDashboardDummyModel Dashboard_Dummy = new Models.ProfileDummies.IndiDashboardDummyModel
        {
            totalProfit = 1200,
            profitMinusTips = 1100,
            totalOrders = 80,
            totalHoursWorking = 90,
            totalDaysWorking = 13,
            ordersCounterPerDayActive = new List<int> { 2, 5, 3, 10, 4, 3, 6, 5, 2, 15, 14, 6, 5 },
            tips = 100,
            ordersCounterPerDayType = new List<int> { 24, 10, 8, 6, 7, 7, 18 }
        };


        public IActionResult DashboardIndexIndi()
        {
            return View(Dashboard_Dummy);
        }
    }
}