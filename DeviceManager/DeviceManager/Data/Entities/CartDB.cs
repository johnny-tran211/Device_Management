using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class CartDB
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Fullname has less than 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string ProductName { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

    }
}
