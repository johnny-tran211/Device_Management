using DeviceManager.Data.Entities;
using DeviceManager.Interface;
using DeviceManager.Models;
using DeviceManager.Models.Item;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Services
{

    public class CartService : ICart
    {
        private List<Cart> Cart { set; get; }

        public int AddToCart(CusItemVM item, int quantity)
        {
            if (Cart == null)
            {
                Cart = new List<Cart>();
            }
            var existCart = Cart.Find(x => x.Item.ProductName == item.ProductName);
            if (existCart == null)
            {
                if (quantity > item.Quantity) return 1;
                existCart = new Cart()
                {
                    Item = new CusItemVM()
                    {
                        Id = item.Id,
                        ProductName = item.ProductName,
                        Image = item.Image,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        DiscountPrice = item.DiscountPrice,
                    },
                  
                    Quantity = quantity,
                };
                Cart.Add(existCart);
            }
            else
            {
                if ((existCart.Quantity + quantity) > existCart.Item.Quantity )
                {
                    return 1;
                }
                else
                {
                existCart.Quantity += quantity;
                }
            }
            return 0;
        }

        public int DeleteItems(int productId)
        {
            var existCart = Cart.Find(x => x.Item.Id == productId);
            if (existCart != null)
            {
                Cart.Remove(existCart);
                if(Cart.Count == 0)
                {
                    Cart = null;
                }
                return 0;
            }

            return 1;

        }


        public List<Cart> GetItemCart()
        {
            return Cart;
        }

        public void RemoveCart()
        {
            Cart = new List<Cart>();
        }
    }
}
