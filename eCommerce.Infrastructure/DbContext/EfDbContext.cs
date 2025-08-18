using Microsoft.EntityFrameworkCore;
using eCommerce.Core.Entities;  // Make sure this namespace points to where your entities like ApplicationUser are

namespace eCommerce.Infrastructure.DbContext
{
    public class EfDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
        }

        // Add your DbSets here:
        public DbSet<ApplicationUser> Users { get; set; } = null!;

        // Uncomment and add other DbSets as you create entities
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: customize table names, relationships, keys, etc.
            // Example: map ApplicationUser to "Users" table explicitly
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");

            // Further configuration for Product, Order, etc. here
            // modelBuilder.Entity<Product>().ToTable("Products");
            // modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }
}
