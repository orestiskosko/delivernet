using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Models
{
    public class OrderModel
    {
       // [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? FloorNo { get; set; }
        public string DoorName { get; set; }
        public int PaymentTypeId { get; set; }
      
    }
}
