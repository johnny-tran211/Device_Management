using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Models.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Controllers
{
    public class CustomerIndexController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerIndexController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<CusItemVM> listItem = await _context.Items.Select(x => new CusItemVM
            {
                Id = x.ItemId,
                ProductName = x.ProductName,
                Image = x.Image,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
            }).ToListAsync();
            return View(listItem);
        }
    }
}