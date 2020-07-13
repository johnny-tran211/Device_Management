using DeviceManager.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class Cart
    {
        public CusItemVM Item { get; set; }
        public int Quantity { get; set; }
    }
}
