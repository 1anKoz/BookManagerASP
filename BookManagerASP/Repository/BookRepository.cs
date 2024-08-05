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

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }
    }
}
