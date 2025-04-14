using Microsoft.EntityFrameworkCore;
using POS_API.Models;
using POS_API.Models;
using System.Numerics;

namespace POS_API.Data
{
    public class PosDbContext: DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }
        public DbSet<SalesProduct> SalesProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent circular cascade delete between Sales and SalesDetails
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.SalesDetails)
                .WithOne(sd => sd.Sales)
                .HasForeignKey<Sales>(s => s.SalesDetailsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesDetails>()
                .HasOne(sd => sd.Sales)
                .WithOne(s => s.SalesDetails)
                .HasForeignKey<SalesDetails>(sd => sd.SalesId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product -> Category (many-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product -> Supplier (many-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // SalesDetails -> Product (many-to-one)
            modelBuilder.Entity<SalesDetails>()
                .HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // SalesProduct -> Product (many-to-one)
            modelBuilder.Entity<SalesProduct>()
                .HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // SalesProduct -> Sales (many-to-one)
            modelBuilder.Entity<SalesProduct>()
                .HasOne(sp => sp.Sales)
                .WithMany()
                .HasForeignKey(sp => sp.SalesId)
                .OnDelete(DeleteBehavior.Cascade);

            // Token -> Users (many-to-one)
            modelBuilder.Entity<Token>()
                .HasOne(t => t.Users)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }



        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}
