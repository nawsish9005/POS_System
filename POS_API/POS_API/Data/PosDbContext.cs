using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_API.Models;

namespace POS_API.Data
{
    public class PosDbContext : IdentityDbContext<IdentityUser>
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Payment> SalesDetails { get; set; }
        public DbSet<SaleItem> SalesProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 👈 REQUIRED for Identity

        }
    }
}
