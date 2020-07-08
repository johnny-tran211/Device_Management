using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "RoomName has less than 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string RoomName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public List<Item> Items { get; set; }
    }
}
