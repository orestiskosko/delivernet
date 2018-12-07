using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;
using Microsoft.AspNetCore.Identity;

namespace DeliverNET.Data
{
    // Add profile data for application users by adding properties to the DeliverNETUser class
    public class DeliverNETUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public BusinessOwner BusinessOwner { get; set; }
        public BusinessCashier BusinessCashier { get; set; }
        public Deliverer Deliverer { get; set; }
    }
}
