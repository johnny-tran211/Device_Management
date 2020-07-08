using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Data.Entities
{
    public class History
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }

        public int ItemId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string RequirementUser { get; set; }

        public DateTime RequirementDateStart { get; set; }

        public DateTime RequirementDateEnd { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string RequirementNote { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FixedUser { get; set; }

        public DateTime FixedDateStart { get; set; }
        public DateTime FixedDateEnd { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FixedNote { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Result { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
    }
}
