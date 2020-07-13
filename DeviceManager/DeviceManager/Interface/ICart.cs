﻿using DeviceManager.Models;
using DeviceManager.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Interface
{
    public interface ICart
    {
        List<Cart> GetItemCart();

        int AddToCart(CusItemVM item, int quantity);
        int DeleteItems(int productId);
        void RemoveCart();
    }
}
