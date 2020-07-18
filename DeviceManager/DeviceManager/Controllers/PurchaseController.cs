using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Models.Cart;
using DeviceManager.Models.Purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Controllers
{
    [Authorize(Roles = "User")]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            var purchases = await _context.CheckOuts.Where(c => !c.Status.Equals("Done")).Select(x => new PurchaseVM() { 
                CartId = x.CartId,
                BuyDate = x.BuyDate,
                Status = x.Status,
                Total = x.Total,
            }).ToListAsync();
            return View(purchases);
        }

        public IActionResult GetDetails(Guid cartId)
        {
            var items = (from c in _context.CartDBs
                        join i in _context.Items on c.ProductName equals i.ProductName
                        where(cartId == c.Id)
                        select new CartObject
                        {
                            Id = c.Id,
                            Image = i.Image,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            DiscountPrice = i.DiscountPrice,
                            ItemQuantity = i.Quantity,
                            Price = i.Price,
                        }).ToList();
            return View(items);
        }
    }
}