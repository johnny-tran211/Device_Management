using System;
using System.Collections.Generic;
using System.Text;
using DeviceManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(p => p.Room)
                .WithMany(b => b.Items).HasForeignKey(k => k.RoomId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<History> Histories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
