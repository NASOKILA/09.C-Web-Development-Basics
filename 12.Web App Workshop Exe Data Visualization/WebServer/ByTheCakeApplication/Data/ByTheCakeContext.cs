namespace HTTPServer.ByTheCakeApplication.Data
{
    using HTTPServer.ByTheCakeApplication.Models;
    using Microsoft.EntityFrameworkCore;
    

    public class ByTheCakeContext : DbContext
    {
        /*
         Trqbva da suzdam baza i da opisha tablicite
         Configurirame bazata:
         Purvo si pravim tablicite i posle tuk gi konfigurirame.
            
         */
        public ByTheCakeContext()
        {}



        //kazvame mu kakvo tabici imame
        public DbSet<User> Users { get; set; }
            
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }


        //TRQBVA NI KONNEKTION STRING
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            const string connectionString = Connection.ConnectionString;

            //ako ne e konfigurirano
            if (!optionsBuilder.IsConfigured) {

                //Za .UseSqlServer() ni trqbva paketa Microsoft.EntityFrameworkCore.SqlServer 
                //I Microsoft.EntityFrameworkCore.SqlServer.Design !!!!
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        //PURVO NI TRQBVA  PKETA Microsoft.EntityFrameworkCore.Tools
        //ZA DA SUZDADEM BAZATA NI TRQBVA DA NAPRAVIM MIGRACIQ, Otvarqme NuGet Package Console
        //Opisvame kude shte pravim imgraciq, v sluchaq si pravim papka 'Migrations' v 'Data' papkata i q setvame kato default progect ot konzolata
        //I Pishem:     Add-Migration InitialMigration
        //Samo si suzdade papka Migrations i soji dva faila v neq

        //configurirame i opisvame vruzkite
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //vseki produkt ima mnogo orderi i edin product sus foreignKey
            modelBuilder.Entity<Product>()
                .HasMany(product => product.Orders)
                .WithOne(order => order.Product)
                .HasForeignKey(productOrder => productOrder.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithOne(product => product.Order)
                .HasForeignKey(productOrder => productOrder.ProductId);

            //opravqme i mejdinata vruzka mejdu dvete tablici s kompoziten kluch
            modelBuilder.Entity<ProductOrder>()
                .HasKey(key => new { key.ProductId, key.OrderId });


            base.OnModelCreating(modelBuilder);
        }
    }
}
