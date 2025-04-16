using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_API.Models;
using POS_API.Models;
using System.Numerics;

namespace POS_API.Data
{
    public class PosDbContext: IdentityDbContext<IdentityUser>
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
            // One-to-One: Sales <-> SalesDetails
            modelBuilder.Entity<SalesDetails>()
                .HasOne(sd => sd.Sales)
                .WithMany(s => s.SalesDetails)
                .HasForeignKey(sd => sd.SalesId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SalesDetails>()
                .HasOne(sd => sd.Sales)
                .WithMany(s => s.SalesDetails)
                .HasForeignKey(sd => sd.SalesId)
                .OnDelete(DeleteBehavior.Restrict);


            // Many-to-One: Product -> Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: Product -> Supplier
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: Product -> Branch
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Branches)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: SalesDetails -> Product
            modelBuilder.Entity<SalesDetails>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SalesDetails)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: SalesProduct -> Product
            modelBuilder.Entity<SalesProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SalesProducts)
                .HasForeignKey(sp => sp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: SalesProduct -> Sales
            modelBuilder.Entity<SalesProduct>()
                .HasOne(sp => sp.Sales)
                .WithMany(s => s.SalesProducts)
                .HasForeignKey(sp => sp.SalesId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: Token -> Users
            modelBuilder.Entity<Token>()
                .HasOne(t => t.Users)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite Key: SalesProduct
            modelBuilder.Entity<SalesProduct>()
                .HasKey(sp => new { sp.SalesId, sp.ProductId });
        }




        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}
