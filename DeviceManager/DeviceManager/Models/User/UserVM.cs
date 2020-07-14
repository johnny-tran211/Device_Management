using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models.User
{
    public class UserVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Fullname has less than 100 characters")]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
