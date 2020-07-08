using DeviceManager.Interface;
using DeviceManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Services
{
    public class CartService : ICart
    {
        private CartViewModel Cart { set; get; }


        public void AddRooms(List<RoomViewModel> rooms)
        {
            Cart.ListRoom = new List<RoomViewModel>();
            Cart.ListRoom = rooms;
        }

        public int AddToCart(Cart item)
        {
            if (Cart.ListCart == null)
            {
                Cart.ListCart = new List<Cart>();
            }
            var existCart = Cart.ListCart.Find(x => x.ProductName == item.ProductName);
            if (existCart == null)
            {
                existCart = new Cart()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = item.ProductName,
                };
                Cart.ListCart.Add(existCart);
                return 0;
            }
            return 1;
        }

        public int DeleteItems(string productId)
        {
            var existCart = Cart.ListCart.Find(x => x.ProductId == productId);
            if (existCart != null)
            {
                Cart.ListCart.Remove(existCart);
                if(Cart.ListCart.Count == 0)
                {
                    Cart.ListCart = null;
                }
                return 0;
            }

            return 1;

        }

        public CartViewModel GetCart()
        {
            return Cart;
        }

        public List<Cart> GetItemCart()
        {
            return Cart.ListCart;
        }

        public List<RoomViewModel> GetRooms()
        {
            return Cart.ListRoom;
        }

        public void RemoveCart()
        {
            Cart = new CartViewModel();
        }
    }
}
