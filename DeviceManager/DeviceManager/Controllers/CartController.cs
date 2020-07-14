using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Data.Entities;
using DeviceManager.Interface;
using DeviceManager.Models;
using DeviceManager.Models.Cart;
using DeviceManager.Models.Item;
using DeviceManager.Models.User;
using DeviceManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeviceManager.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public CartController(ICart cart, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _cart = cart;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User) && HttpContext.Session.GetString("Cart") == null)
            {

                List<CartObject> carts = (from i in _context.Items
                             join ca in _context.CartDBs on i.ProductName equals ca.ProductName
                             where ca.Status.Equals("Waiting")
                             where ca.Email.Equals(User.Identity.Name)
                             select new CartObject
                             {
                                 Id = ca.Id,
                                 ProductName = i.ProductName,
                                 Image = i.Image,
                                 Price = i.Price,
                                 DiscountPrice = i.DiscountPrice,
                                 ItemQuantity = i.Quantity,
                                 Quantity = ca.Quantity
                             }).ToList();
                if (carts.Count > 0)
                {
                    CartList cl = _cart.ChangeObjToCartList(carts);
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cl));
                    return View(cl);
                }
                else
                {
                    ViewBag.ERRORMESSAGE = "List is empty";
                    return View();
                }
            }
            if (HttpContext.Session.GetString("Cart") == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                ViewBag.STATUS = TempData["Failed"];
                return View();
            }
            var cart = JsonConvert.DeserializeObject<CartList>(HttpContext.Session.GetString("Cart"));
            if (cart == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return View();
            }
            return View(cart);
        }

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(string returnUrl, [Bind("ProductName")] CusItemVM cartItem, int quantity)
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                _cart.RemoveCart();
            }
            var item = _context.Items.Where(i => i.ProductName == cartItem.ProductName).Select(s => new CusItemVM()
            {
                ProductName = s.ProductName,
                Image = s.Image,
                Price = s.Price,
                DiscountPrice = s.DiscountPrice,
                Quantity = s.Quantity,
            }).FirstOrDefault();
            int result = _cart.AddToCart(item, quantity);
            if (result == 0)
            {
                //if (_signInManager.IsSignedIn(User))
                //{
                //    _context.CartDBs.Add();
                //}
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
        public IActionResult Delete(string productName)
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                ViewBag.ERRORMESSAGE = "List is empty";
                return RedirectToAction(nameof(Index));
            }
            var cart = JsonConvert.DeserializeObject<CartList>(HttpContext.Session.GetString("Cart"));
            int result = _cart.DeleteItems(productName);
            if (result == 0)
            {
                ViewBag.STATUS = "Delete Successful !!!";
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart.GetItemCart()));
            }
            else if (result == 2)
            {
                HttpContext.Session.Clear();
            }
            else
            {
                ViewBag.STATUS = "Delete Failed !!!";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            if (HttpContext.Session.GetString("Cart") == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var currentUser = await _userManager.GetUserAsync(User);
            UserVM user = new UserVM()
            {
                Email = currentUser.Email,
                FullName = currentUser.FullName,
                Address = currentUser.Address,
                City = currentUser.City,
                Country = currentUser.Country,
                PhoneNumber = currentUser.PhoneNumber,
            };
            return View(user);
        }

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut([Bind("Email, FullName, Address, City, Country, PhoneNumber")] UserVM userVM)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Session.GetString("Cart") == null)
                {
                    TempData["Failed"] = "Checkout Failed !!!";
                    return RedirectToAction(nameof(Index));
                }
                var cart = JsonConvert.DeserializeObject<CartList>(HttpContext.Session.GetString("Cart"));

            }

            return View(userVM);
        }
    }
}