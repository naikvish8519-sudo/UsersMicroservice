using Microsoft.EntityFrameworkCore;
using eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommerce.Infrastructure.DbContext

{
    public class EfDbContext : Microsoft.EntityFrameworkCore.DbContext

    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");

        }
    }
}
