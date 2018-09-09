namespace KittenApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using KittenApp.Models;

    public class KittenAppDbContext : DbContext
    {

        public KittenAppDbContext()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

        public DbSet<Breed> Breads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        
        //connections
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
            
            modelBuilder.Entity<Breed>()
                .HasMany(b => b.Kittens)
                .WithOne(k => k.Breed);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
