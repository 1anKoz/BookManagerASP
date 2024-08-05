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
        public DbSet<BookUserReview> BookUserReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookUserReview>()
                .HasKey(bur => new { bur.UserEntityId, bur.ReviewId, bur.BookId });
            modelBuilder.Entity<BookUserReview>()
                .HasOne(u => u.UserEntity)
                .WithMany(bur => bur.BookUserReviews)
                .HasForeignKey(u => u.UserEntityId);
            modelBuilder.Entity<BookUserReview>()
                .HasOne(r => r.Review)
                .WithMany(bur => bur.BookUserReviews)
                .HasForeignKey(r => r.ReviewId);
            modelBuilder.Entity<BookUserReview>()
                .HasOne(b => b.Book)
                .WithMany(bur => bur.BookUserReviews)
                .HasForeignKey(b => b.BookId);
        }
    }
}
