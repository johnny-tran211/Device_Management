using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Interface;
using DeviceManager.Models;
using DeviceManager.Models.Item;
using DeviceManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeviceManager.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly ApplicationDbContext _context;
        public CartController(ICart cart, ApplicationDbContext context)
        {
            _cart = cart;
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            var cart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("Cart"));
            if (cart == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            return View(cart);
        }

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(string returnUrl, [Bind("Id")] CusItemVM cartItem, int quantity)
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                _cart.RemoveCart();
            }
            var item = _context.Items.Where(i => i.ItemId == cartItem.Id).Select(s => new CusItemVM()
            {
                Id = s.ItemId,
                ProductName = s.ProductName,
                Image = s.Image,
                Price = s.Price,
                DiscountPrice = s.DiscountPrice,
                Quantity = s.Quantity,
            }).FirstOrDefault();
            int result = _cart.AddToCart(item, quantity);
            if (result == 0)
            {
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart.GetItemCart()));
            }
            else if (result == 1)
            {
                TempData["Error"] = "Product is out of stock";
            }
            return Redirect(returnUrl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            if (HttpContext.Session.GetString("Cart") == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            int result = _cart.DeleteItems(id);
            if (result == 0)
            {
                ViewBag.STATUS = "Delete Successful !!!";
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart.GetItemCart()));
            }
            else
            {
                ViewBag.STATUS = "Delete Failed !!!";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}