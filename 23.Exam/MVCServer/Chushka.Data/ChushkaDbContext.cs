namespace Chushka.Data
{
    using Chushka.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChushkaDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Models.Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role);



            modelBuilder.Entity<Product>()
                .HasOne(t => t.Type);



            modelBuilder.Entity<Order>()
                .HasOne(t => t.Client)
                .WithMany(c => c.Orders);
            
            
            modelBuilder.Entity<Order>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Orders);
            

            base.OnModelCreating(modelBuilder);
        }



    }
}
