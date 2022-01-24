using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product.Data.Configurations;
using Product.Data.Entities;

namespace Product.Data.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<Entities.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<Entities.Product>().HasData(
                new Entities.Product { Id = 1, Name = "P1", Price = 11.99, Count = 10 },
                new Entities.Product { Id = 2, Name = "P2", Price = 11.99, Count = 10 },
                new Entities.Product { Id = 3, Name = "P3", Price = 11.99, Count = 10 },
                new Entities.Product { Id = 4, Name = "P4", Price = 11.99, Count = 10 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
