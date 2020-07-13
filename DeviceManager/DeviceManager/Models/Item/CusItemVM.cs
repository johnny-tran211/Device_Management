using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models.Item
{
    public class CusItemVM
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public int Quantity { get; set; }
    }
}
