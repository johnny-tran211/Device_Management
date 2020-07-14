using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Data;
using DeviceManager.Models;
using DeviceManager.Models.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Controllers
{
    public class CustomerIndexController : Controller
    {
        private const int PAGE_SIZE = 4;
        private readonly ApplicationDbContext _context;
        public CustomerIndexController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<CusItemVM> listItem = await _context.Items.Select(x => new CusItemVM
            {
                ProductName = x.ProductName,
                Image = x.Image,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
            }).Take(4).ToListAsync();
            return View(listItem);
        }

        [HttpGet]
        public async Task<IActionResult> AllItems(int pageIndex)
        {
            pageIndex = (pageIndex > 0) ? pageIndex : 1;

            var totalRecord = await _context.Items.CountAsync();
            var items = await _context.Items.Skip((pageIndex - 1) * PAGE_SIZE).Take(PAGE_SIZE)
            .Select(x => new CusItemVM()
            {
                ProductName = x.ProductName,
                Image = x.Image,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
            }).ToListAsync();
            var pagination = new Pagination<CusItemVM>
            {
                Items = items,
                TotalRecords = Math.Ceiling((decimal)totalRecord / PAGE_SIZE),
                pageSize = pageIndex,
            };
            return View(pagination);
        }
    }
}