using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class CartViewModel
    {
        public List<Cart> ListCart { get; set; }
        public List<RoomViewModel> ListRoom { get; set; }
    }
}
