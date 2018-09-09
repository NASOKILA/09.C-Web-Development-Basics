namespace HTTPServer.ByTheCakeApplication.Data
{
    using HTTPServer.ByTheCakeApplication.Models;
    using Microsoft.EntityFrameworkCore;
    

    public class ByTheCakeContext : DbContext
    {
        public ByTheCakeContext()
        {}

        public DbSet<User> Users { get; set; }
            
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = Connection.ConnectionString;

            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(product => product.Orders)
                .WithOne(order => order.Product)
                .HasForeignKey(productOrder => productOrder.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithOne(product => product.Order)
                .HasForeignKey(productOrder => productOrder.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(key => new { key.ProductId, key.OrderId });


            base.OnModelCreating(modelBuilder);
        }
    }
}
