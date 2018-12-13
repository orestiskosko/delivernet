<<<<<<< HEAD:DeliverNET/DeliverNET/Models/OrderModel.cs
﻿using Newtonsoft.Json;
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
        public int OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int FloorNo { get; set; }
        public string DoorName { get; set; }
        public int PaymentTypeId { get; set; }
        public string State { get; set; } // 1.Pending | 2.Accepted | 3.Picked-Up | 4.Delivered | 5.Failed
        public float DistanceToShop { get; set; } // Deliverer - Shop
        public float ETA { get; set; } // Deliverer - Shop
        public float Price { get; set; }
    }
}
=======
﻿using System;
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
    }
}
>>>>>>> develop:DeliverNET/DeliverNET/Models/ProfileBusiViewModels/OrderBusiViewModel.cs
