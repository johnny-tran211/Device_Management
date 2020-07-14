using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models.Cart
{
    public class CartList
    {
        public List<Cart> Carts { get; set; }
        public double TotalPrice { get; set; }
        public double EstimatedShipping { get; set; }
        public double TotalWShipFee { get; set; }
    }
}
