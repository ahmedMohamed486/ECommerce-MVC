using ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Configuration
{
    public class ECommerceDbContext :IdentityDbContext<ApplicationUser>
    {
        public ECommerceDbContext()
        { }
        public ECommerceDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=A7MED;Database=ECommerceDB;TrustServerCertificate=True;Integrated Security=True;");



        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetails> ordersDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfig());



            base.OnModelCreating(builder);
        }

    }
}
