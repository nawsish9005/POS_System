using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_API.Models;

namespace POS_API.Data
{
    public class PosDbContext : IdentityDbContext<User>
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category - Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier - Stock
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Supplier)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock - Branch
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Branches)
                .WithMany()
                .HasForeignKey(s => s.BranchesId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock - Product
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseItem - Stock
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Stock)
                .WithMany(s => s.PurchaseItems)
                .HasForeignKey(pi => pi.StockId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseItem - Product
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale - SaleItem
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // SaleItem - Product
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale - User
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale - Customer
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Product - Branch
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Branches)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment - Sale
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Sale)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoyaltyPoint - Customer
            modelBuilder.Entity<LoyaltyPoint>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(lp => lp.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
