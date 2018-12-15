﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Models.ProfileBusiViewModels
{
    public class OrderBusiViewModel
    {
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int? FloorNo { get; set; }

        [DataType(DataType.Text)]
        public string DoorName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int PaymentTypeId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}