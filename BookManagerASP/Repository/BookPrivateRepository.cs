using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookManagerASP.Repository
{
    public class BookPrivateRepository : IBookPrivateRepository
    {
        private readonly DataContext _context;
        public BookPrivateRepository(DataContext context)
        {
            _context = context;
        }

        public bool BookPrivateExists(int? bookPrivateId)
        {
            return _context.Books.Any(bp => bp.Id == bookPrivateId);
        }

        public ICollection<BookPrivate> GetAllBookPrivates()
        {
            return _context.BooksPrivates.OrderBy(bp => bp.Id).ToList();
        }

        public BookPrivate GetBookPrivate(int? bookPrivateId)
        {
            return _context.BooksPrivates.Where(
                bp => bp.Id == bookPrivateId).FirstOrDefault();
        }

        public BookPrivate GetBookPrivate(string title = null)
        {
            var query = _context.BooksPrivates.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(bp => bp.Book.Title == title);
            }

            return query.FirstOrDefault();
        }

        public ICollection<BookPrivate> GetBookPrivatesByBookAuthor(string author)
        {
            return _context.BooksPrivates.Where(
                b => b.Book.Author == author).OrderBy(b => b.Id).ToList();
        }

        public ICollection<BookPrivate> GetBookPrivatesOnShelf(int shelfId)
        {
            return _context.BooksPrivates.Where(b => b.ShelfId == shelfId).ToList();
        }

        public ICollection<BookPrivate> GetFavouriteBookPrivates()
        {
            return _context.BooksPrivates.Where(bp => bp.IsFavourite == true)
                .OrderBy(bp => bp.Id).ToList();
        }

        public ICollection<BookPrivate> GetUserBookPrivates(string userId)
        {
            return _context.BooksPrivates.Where(
                bp => bp.Shelf.UserEntityId == userId).ToList();
        }


        public bool CreateBookPrivate(BookPrivate bookPrivate)
        {
            _context.Add(bookPrivate);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBookPrivate(int bookId, int shelfId, BookPrivate bookPrivate)
        {
            _context.Update(bookPrivate);
            return Save();
        }
    }
}
