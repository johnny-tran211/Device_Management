using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Interface;
using DeviceManager.Models;
using DeviceManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeviceManager.Controllers
{
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
            var cart = JsonConvert.DeserializeObject<CartViewModel>(HttpContext.Session.GetString("Cart"));
            if (cart.ListCart == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            if (_cart.GetRooms() == null)
            {
                List<RoomViewModel> listroom = _context.Rooms.Select(x => new RoomViewModel()
                {
                    Id = x.RoomId,
                    RoomName = x.RoomName,
                    Status = x.Status,
                }).ToList();
                _cart.AddRooms(listroom);
                cart = _cart.GetCart();
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }

            return View(cart);
        }

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(string returnUrl, [Bind("ProductName")] Cart cartItem)
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                _cart.RemoveCart();
            }
            var item = new Cart()
            {
                ProductName = cartItem.ProductName,
                RoomId = cartItem.RoomId,
            };
            int result = _cart.AddToCart(item);
            if (result == 0)
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart.GetCart()));
            else
            {
                TempData["AddError"] = "Product existed !!!";
            }
            return Redirect(returnUrl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            
            if (HttpContext.Session.GetString("Cart") == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            int result =  _cart.DeleteItems(id);
            if (result == 0)
            {
                ViewBag.STATUS = "Delete Successful !!!";
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart.GetCart()));
            }
            else
            {
                ViewBag.STATUS = "Delete Failed !!!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}