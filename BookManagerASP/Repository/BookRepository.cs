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

            if (query.Title != null)
                return book.Any(b => b.Title == query.Title);
            else if (query.Isbn != null)
                return book.Any(b => b.Isbn == query.Isbn);
            else if (query.Id != null)
                return book.Any(b => b.Id == query.Id);
            else if (query.Author != null)
                return book.Any(b => b.Author == query.Author);
            else if (query.Genre != null)
                return book.Any(b => b.Genre == query.Genre);

            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public Book GetBook(BookQuery query)
        {
            var book = _context.Books;
            if(query.Title != null)
                book.Where(b => b.Title == query.Title);
            else if (query.Isbn != null)
                book.Where(b => b.Isbn == query.Isbn);
            else if (query.Id != null)
                book.Where(b => b.Id == query.Id);

            return book.FirstOrDefault();
        }

        public ICollection<Book> GetBooks(BookQuery query)
        {
            var book = _context.Books;
            if (query.Title != null)
                book.Where(b => b.Title == query.Title);
            if (query.Author != null)
                book.Where(b => b.Author == query.Author);
            if (query.Isbn != null)
                book.Where(b => b.Isbn == query.Isbn);
            if (query.Id != null)
                book.Where(b => b.Id == query.Id);
            if (query.Genre != null)
                book.Where(b => b.Genre == query.Genre);

            return book.ToList();
        }

        //public ICollection<Book> GetBooks()
        //{
        //    return _context.Books.OrderBy(b => b.Id).ToList();
        //}

        //public ICollection<Book> GetBooksByAuthor(string author)
        //{
        //    return _context.Books.Where(b => b.Author == author).OrderBy(b => b.Id).ToList();
        //}

        //public Book GetBook(int id)
        //{
        //    return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        //}

        //public Book GetBook(string title = null/*, string author = null*/)
        //{
        //    var query = _context.Books.AsQueryable();

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        query = query.Where(b => b.Title == title);
        //    }

        //    //if (!string.IsNullOrEmpty(author))
        //    //{
        //    //    query = query.Where(b => b.Author == author);
        //    //}

        //    return query.FirstOrDefault();
        //}

        //public int GetBookRating(int id)
        //{
        //    var review = _context.Reviews.Where(b => b.Id == id);

        //    if (review.Count() <= 0)
        //    {
        //        return 0;
        //    }
        //    return (int)review.Sum(r => r.Rating) / review.Count();
        //}


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
