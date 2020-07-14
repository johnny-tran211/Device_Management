using DeviceManager.Data.Entities;
using DeviceManager.Interface;
using DeviceManager.Models;
using DeviceManager.Models.Cart;
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
        private CartList CheckOutCart { set; get; }

        public int AddToCart(CusItemVM item, int quantity)
        {
            if (CheckOutCart == null)
            {
                CheckOutCart = new CartList();
                CheckOutCart.Carts = new List<Cart>();
                CheckOutCart.TotalPrice = 0;
                CheckOutCart.EstimatedShipping = 5;
            }
            var existCart = CheckOutCart.Carts.Find(x => x.Item.ProductName == item.ProductName);
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
                CheckOutCart.TotalPrice = CheckOutCart.TotalPrice + (existCart.Item.DiscountPrice * quantity);
                CheckOutCart.TotalWShipFee = CheckOutCart.TotalPrice + CheckOutCart.EstimatedShipping;
                CheckOutCart.Carts.Add(existCart);
            }
            else
            {
                if ((existCart.Quantity + quantity) > existCart.Item.Quantity)
                {
                    return 1;
                }
                else
                {
                    existCart.Quantity += quantity;
                    CheckOutCart.TotalPrice = CheckOutCart.TotalPrice + (existCart.Item.DiscountPrice * quantity);
                    CheckOutCart.TotalWShipFee = CheckOutCart.TotalPrice + CheckOutCart.EstimatedShipping;
                }
            }
            return 0;
        }

        public int DeleteItems(int productId)
        {
            var existCart = CheckOutCart.Carts.Find(x => x.Item.Id == productId);
            if (existCart != null)
            {
                CheckOutCart.Carts.Remove(existCart);
                if (CheckOutCart.Carts.Count == 0)
                {
                    CheckOutCart = null;
                }
                return 0;
            }

            return 1;

        }


        public CartList GetItemCart()
        {
            return CheckOutCart;
        }

        public void RemoveCart()
        {
            CheckOutCart = new CartList();
            CheckOutCart.Carts = new List<Cart>();
            CheckOutCart.EstimatedShipping = 5;
        }

        public void SetItemCart(CartList cartList)
        {
            CheckOutCart = cartList;
        }
    }
}
