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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
            .HasIndex(u => u.ProductName)
            .IsUnique();
            modelBuilder.Entity<CartDB>()
                .HasKey(c => new { c.Id, c.ProductName });
        }
        public DbSet<History> Histories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CartDB> CartDBs { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
    }
}
