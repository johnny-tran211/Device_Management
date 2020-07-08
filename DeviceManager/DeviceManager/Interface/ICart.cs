using DeviceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Interface
{
    public interface ICart
    {
        List<Cart> GetItemCart();

        CartViewModel GetCart();

        int AddToCart(Cart item);
        int DeleteItems(string productId);
        void RemoveCart();
        void AddRooms(List<RoomViewModel> rooms);
        List<RoomViewModel> GetRooms();
    }
}
