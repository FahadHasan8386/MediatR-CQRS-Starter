using Microsoft.EntityFrameworkCore;
using MiniCQRS.API.Models;

namespace MiniCQRS.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("1"),
                    Name = "Laptop",
                    Description = "High-performance laptop",
                    Price = 999.99m,
                    Stock = 10,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.Parse("2"),
                    Name = "Mouse",
                    Description = "Wireless mouse",
                    Price = 29.99m,
                    Stock = 50,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
