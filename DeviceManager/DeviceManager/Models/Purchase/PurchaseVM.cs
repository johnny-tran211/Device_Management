using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models.Purchase
{
    public class PurchaseVM
    {
        public Guid CartId { get; set; }
        public double Total { get; set; }
        public DateTime BuyDate { get; set; }
        public string Status { get; set; }
    }
}
