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


        public async Task<bool> BookExistsAsync(BookQuery query)
        {
            var book = _context.Books;

            if (query.Isbn != null)
                return await book.AnyAsync(b => b.Isbn == query.Isbn);
            else if (query.Id != null)
                return await book.AnyAsync(b => b.Id == query.Id);

            return false;
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }


        public async Task<Book> GetBookAsync(BookQuery query)
        {
            Book book = new Book();
            if (query.Id != null)
                book = await _context.Books.Where(b => b.Id == query.Id).FirstOrDefaultAsync();
            else if (query.Isbn != null)
                book = await _context.Books.Where(b => b.Isbn == query.Isbn).FirstOrDefaultAsync();
            else if (query.Title != null)
                book = await _context.Books.Where(b => b.Title == query.Title).FirstOrDefaultAsync();

            return book;
        }

        public async Task<ICollection<Book>> GetBooksAsync(BookQuery query)
        {
            List<Book> books = new List<Book>();

            if (query.Title != null && query.Author != null && query.Genre != null)
                books = await _context.Books.Where(b =>
                b.Title == query.Title &
                b.Author == query.Author &
                b.Genre == query.Genre).ToListAsync();

            else if (query.Title != null && query.Author != null)
                books = await _context.Books.Where(b =>
                b.Title == query.Title &
                b.Author == query.Author).ToListAsync();

            else if (query.Title != null && query.Genre != null)
                books = await _context.Books.Where(b =>
                b.Title == query.Title &
                b.Genre == query.Genre).ToListAsync();

            else if (query.Author != null && query.Genre != null)
                books = await _context.Books.Where(b =>
                b.Author == query.Author &
                b.Genre == query.Genre).ToListAsync();

            else if (query.Title != null)
                books = await _context.Books.Where(b => b.Title == query.Title).ToListAsync();

            else if (query.Author != null)
                books = await _context.Books.Where(b => b.Author == query.Author).ToListAsync();

            else if (query.Genre != null)
                books = await _context.Books.Where(b => b.Genre == query.Genre).ToListAsync();

            else
                books = await _context.Books.ToListAsync();

            return books;
        }


        public async Task<bool> CreateBookAsync(Book book)
        {
            await _context.AddAsync(book);
            return await SaveAsync();
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _context.Update(book);
            return await SaveAsync();
        }
    }
}
