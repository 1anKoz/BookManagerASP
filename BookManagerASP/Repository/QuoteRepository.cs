using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookManagerASP.Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly DataContext _context;
        public QuoteRepository(DataContext context)
        {
            _context = context;
        }

        public bool QuoteExists(int quoteId)
        {
            return _context.Quotes.Any(q => q.Id == quoteId);
        }

        public Quote GetQuote(int? quoteId)
        {
            return _context.Quotes.Where(q => q.Id == quoteId).FirstOrDefault();
        }

        public ICollection<Quote> GetQuotes()
        {
            return _context.Quotes.OrderBy(q => q.Id).ToList();
        }

        public ICollection<Quote> GetFavouriteQuotes()
        {
            return _context.Quotes.Where(q => q.IsFavourite == true)
                .OrderBy(q => q.Id).ToList();
        }

        public ICollection<Quote> GetBookPrivateQuotes(int bookPrivateId)
        {
            return _context.Quotes.Where(
                q => q.BookPrivateId == bookPrivateId).ToList();
        }

        public ICollection<Quote> GetUserQuotes(string userId)
        {
            return _context.Quotes.Where(
                q => q.BookPrivate.Shelf.UserEntityId == userId).ToList();
        }


        public bool CreateQuote(Quote quote)
        {
            _context.Add(quote);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuote(Quote quote)
        {
            _context.Update(quote);
            return Save();
        }
    }
}
