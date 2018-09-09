
using Microsoft.EntityFrameworkCore;
using SimpleMvc.Domain;

namespace SimpleMvc.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext()
        {}


        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
