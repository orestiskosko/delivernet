using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverNET.Models.ProfileBusiViewModels
{
    public class VerifyFormBusiViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Credentials { get; set; }
    }
}
