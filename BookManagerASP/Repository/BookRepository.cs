using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookManagerASP.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }


        public bool BookExists(BookQuery query)
        {
            var book = _context.Books;

            if (query.Isbn != null)
                return book.Any(b => b.Isbn == query.Isbn);
            else if (query.Id != null)
                return book.Any(b => b.Id == query.Id);

            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public Book GetBook(BookQuery query)
        {
            Book book = new Book();
            if(query.Id != null)
                book = _context.Books.Where(b => b.Id == query.Id).FirstOrDefault();
            else if (query.Isbn != null)
                book = _context.Books.Where(b => b.Isbn == query.Isbn).FirstOrDefault();
            else if (query.Title != null)
                book = _context.Books.Where(b => b.Title == query.Title).FirstOrDefault();

            return book;
        }

        public ICollection<Book> GetBooks(BookQuery query)
        {
            List<Book> books = new List<Book>();

            if (query.Title != null && query.Author != null && query.Genre != null)
                books = _context.Books.Where(b =>
                b.Title == query.Title &
                b.Author == query.Author &
                b.Genre == query.Genre).ToList();

            else if (query.Title != null && query.Author != null)
                books = _context.Books.Where(b =>
                b.Title == query.Title &
                b.Author == query.Author).ToList();

            else if (query.Title != null && query.Genre != null)
                books = _context.Books.Where(b =>
                b.Title == query.Title &
                b.Genre == query.Genre).ToList();

            else if (query.Author != null && query.Genre != null)
                books = _context.Books.Where(b =>
                b.Author == query.Author &
                b.Genre == query.Genre).ToList();

            else if (query.Title != null)
                books = _context.Books.Where(b => b.Title == query.Title).ToList();

            else if (query.Author != null)
                books = _context.Books.Where(b => b.Author == query.Author).ToList();

            else if (query.Genre != null)
                books = _context.Books.Where(b => b.Genre == query.Genre).ToList();

            else 
                books = _context.Books.ToList();

            return books;
        }


        public bool CreateBook(Book book)
        {
            _context.Add(book);
            return Save();
        }


        public bool UpdateBook(Book book)
        {
            _context.Update(book);
            return Save();
        }
    }
}
