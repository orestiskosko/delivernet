using DeliverNET.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Data
{
    public class Deliverer
    {
        public bool IsValidated { get; set; }
        public string Credentials { get; set; }
        public bool IsWorking { get; set; }
        public bool IsDelivering { get; set; }
        public string OperationalRegion { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }

        // one-to-one with DeliverNETUser
        public string DeliverNetUserId { get; set; }
        public DeliverNETUser DeliverNETUser { get; set; }
    }
}
