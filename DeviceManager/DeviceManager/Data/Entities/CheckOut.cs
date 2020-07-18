using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class CheckOut
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckOutId { get; set; }
        public Guid ShiperId { get; set; }
        [Required]
        public Guid CartId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public double Subtotal { get; set; }
        [Required]
        public double EstimatedShipping { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime BuyDate { get; set; }

    }
}
