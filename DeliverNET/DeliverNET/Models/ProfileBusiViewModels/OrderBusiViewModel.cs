using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Models.ProfileBusiViewModels
{
    public class OrderBusiViewModel
    {
        // [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? FloorNo { get; set; }
        public string DoorName { get; set; }
        public int PaymentTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public float Price { get; set; }
        public string Comments { get; set; }
    }
}