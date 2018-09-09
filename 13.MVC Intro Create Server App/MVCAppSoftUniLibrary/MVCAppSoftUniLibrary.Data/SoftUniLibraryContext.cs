namespace MVCAppSoftUniLibrary.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    public class SoftUniLibraryContext : DbContext
    {

        public SoftUniLibraryContext()
        {}


        //DbSets
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

    }
}
