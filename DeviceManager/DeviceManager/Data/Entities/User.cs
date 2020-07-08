using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "Fullname has less than 100 characters")]
        public string FullName { get; set; }
        [Required]
        public DateTime Dob { get; set; }

    }
}
