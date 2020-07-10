using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Fullname has less than 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Fullname has less than 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Fullname has less than 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }


        [MaxLength(250)]
        [Column(TypeName = "varchar(50)")]
        public string Image { get; set; }

        public DateTime BuyDate { get; set; }

        [Required]
        public DateTime MaintainDate { get; set; }

        public int MaintainTimes { get; set; }
        [Required]
        public double Price { get; set; }
        public double DiscountPrice { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }


        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
