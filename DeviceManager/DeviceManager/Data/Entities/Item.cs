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
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ProductName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }

        public int RoomId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Image { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime MaintainDate { get; set; }

        public int MaintainTimes { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
    }
}
