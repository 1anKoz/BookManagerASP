using BookManagerASP.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BookManagerASP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookPrivate> BooksPrivates { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<>
        }
    }
}
