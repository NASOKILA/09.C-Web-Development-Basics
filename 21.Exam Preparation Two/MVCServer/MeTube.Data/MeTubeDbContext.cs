namespace MeTube.Data
{
    using MeTybe.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class MeTubeDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured){
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
                .HasMany(u => u.Tubes)
                .WithOne(t => t.Uploader);

            modelBuilder.Entity<Tube>()
                .HasOne(t => t.Uploader)
                .WithMany(u => u.Tubes);


            base.OnModelCreating(modelBuilder);
        }

    }
}
