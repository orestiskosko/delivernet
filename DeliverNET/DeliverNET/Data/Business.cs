using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Data
{
    public class Business
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime SignupDate { get; set; }
        public DateTime VerificationDate { get; set; }
        public bool IsVerified { get; set; }
        public string Credentials { get; set; }
        public bool Active { get; set; }

        public BusinessOwner BusinessOwner { get; set; }
        public BusinessCashier BusinessCashier { get; set; }
    }
}
