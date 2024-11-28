using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 1, ProductName = "Iphone 14", Price = 800, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 2, ProductName = "Iphone 15", Price = 1000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 3, ProductName = "Iphone 16", Price = 1499, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 4, ProductName = "Iphone 16 Pro", Price = 1899, IsActive = false });
        }
        public DbSet<Product> Products { get; set; }
    }
}