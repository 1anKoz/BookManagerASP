using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;

namespace BookManagerASP.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(p => p.Id == bookId);
        }


        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book GetBook(string title = null, string author = null)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title == title);
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author == author);
            }

            return query.FirstOrDefault();
        }

        public int GetBookRating(int id)
        {
            var review = _context.Reviews.Where(b => b.Id == id);

            if (review.Count() <= 0)
            {
                return 0;
            }
            return (int)review.Sum(r => r.Rating) / review.Count();
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }


        public bool CreateBook(Book book)
        {
            _context.Add(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
