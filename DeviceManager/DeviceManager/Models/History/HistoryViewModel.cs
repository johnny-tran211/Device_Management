using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class HistoryViewModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string RequirementUser { get; set; }

        public DateTime RequirementDateStart { get; set; }

        public DateTime RequirementDateEnd { get; set; }

        public string RequirementNote { get; set; }

        public string FixedUser { get; set; }

        public DateTime FixedDateStart { get; set; }
        public DateTime FixedDateEnd { get; set; }

        public string FixedNote { get; set; }

        public string Result { get; set; }

        public string Status { get; set; }
    }
}
