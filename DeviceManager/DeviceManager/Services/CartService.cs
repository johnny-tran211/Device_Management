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
        private Guid IdForCart { set; get; }

        public int AddToCart(CusItemVM item, int quantity)
        {
            var existCart = CheckOutCart.Carts.Find(x => x.Item.ProductName.Equals(item.ProductName));
            if (existCart == null)
            {
                if (quantity > item.Quantity) return 1;
                existCart = new Cart()
                {
                    CartId = (IdForCart == null)? Guid.NewGuid() : IdForCart,
                    Item = new CusItemVM()
                    {
                        ProductName = item.ProductName,
                        Image = item.Image,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        DiscountPrice = item.DiscountPrice,
                    },

                    Quantity = quantity,
                };
                if (IdForCart == null)
                {
                    IdForCart = existCart.CartId;
                }
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

        public CartList ChangeObjToCartList(List<CartObject> cartObject)
        {
            CartList cartList = new CartList();
            cartList.Carts = new List<Cart>();

            for (var i = 0; i < cartObject.Count; i++)
            {
                cartList.Carts.Add(new Cart()
                {
                    CartId = cartObject[i].Id,
                    Quantity = cartObject[i].Quantity,
                    Item = new CusItemVM()
                    {
                        ProductName = cartObject[i].ProductName,
                        Image = cartObject[i].Image,
                        Price = cartObject[i].Price,
                        DiscountPrice = cartObject[i].DiscountPrice,
                        Quantity = cartObject[i].ItemQuantity,
                    },
                });
                cartList.TotalPrice += Math.Round((cartObject[i].DiscountPrice * cartObject[i].Quantity), 2);
                
            }
            cartList.EstimatedShipping = 5;
            cartList.TotalWShipFee = cartList.TotalPrice + cartList.EstimatedShipping;
            CheckOutCart = cartList;
            IdForCart = cartObject[0].Id;
            return CheckOutCart;
        }

        public int DeleteItems(string productName)
        {
            var existCart = CheckOutCart.Carts.Find(x => x.Item.ProductName.Equals(productName));
            if (existCart != null)
            {
                CheckOutCart.Carts.Remove(existCart);
                if (CheckOutCart.Carts.Count == 0)
                {
                    return 2;
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
            CheckOutCart.TotalPrice = 0;
            CheckOutCart.EstimatedShipping = 5;
            IdForCart = Guid.NewGuid();
        }

        public void SetItemCart(CartList cartList)
        {
            CheckOutCart = cartList;
        }
    }
}
