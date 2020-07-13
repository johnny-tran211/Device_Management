using DeviceManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime MaintainDate { get; set; }

        public int MaintainTimes { get; set; }

        public string Status { get; set; }
        public int Quantity { get; set; }

    }
}

