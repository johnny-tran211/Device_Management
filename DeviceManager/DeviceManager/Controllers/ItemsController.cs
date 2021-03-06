﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManager.Data;
using DeviceManager.Data.Entities;
using DeviceManager.Models;
using DeviceManager.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DeviceManager.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items
                .Where(c => c.Status == Status.Active.ToString())
                .Select(x => new ItemViewModel()
                {
                    Id = x.ItemId,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Type = x.Type,
                    RoomId = x.RoomId,
                    Image = x.Image,
                    BuyDate = x.BuyDate,
                    MaintainDate = x.MaintainDate,
                    MaintainTimes = x.MaintainTimes,
                    Status = x.Status,
                    Room = x.Room,
                }).ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Where(c => c.Status == Status.Active.ToString())
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {

            HttpContext.Session.SetString("ROOMS", JsonConvert.SerializeObject(
                _context.Rooms.Where(c => c.Status == "Active").Select(x => new RoomViewModel()
                {
                    Id = x.RoomId,
                    RoomName = x.RoomName,
                }).ToList()));
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ProductName,Description,Type,RoomId,Image,MaintainDate")] Item item)
        {
            if (_context.Rooms.Where(x => x.RoomId == item.RoomId).Count() == 0)
            {
                ModelState.AddModelError("RoomId", "Room not found");
                return View(item);
            }
            if (ModelState.IsValid)
            {
                item.MaintainTimes = 0;
                item.Status = Status.Active.ToString();
                item.BuyDate = DateTime.Now;
                if (item.MaintainDate < item.BuyDate)
                {
                    if (HttpContext.Session.GetString("ROOMS") == null)
                    {
                        HttpContext.Session.SetString("ROOMS", JsonConvert.SerializeObject(
                           _context.Rooms.Where(c => c.Status == "Active").Select(x => new RoomViewModel()
                           {
                               Id = x.RoomId,
                               RoomName = x.RoomName,
                           }).ToList()));
                    }
                    ModelState.AddModelError("MaintainDate", "MaintainDate have to after today");
                    return View(item);
                }
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Where(c => c.Status == Status.Active.ToString())
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ProductName,Description,Type,RoomId,Image,Status")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Where(c => c.Status == Status.Active.ToString())
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            item.Status = Status.Disable.ToString();
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
