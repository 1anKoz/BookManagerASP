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

        //public bool QuoteExists(int quoteId)
        //{
        //    return _context.Quotes.Any(q => q.Id == quoteId);
        //}

        public ICollection<Quote> GetQuotes()
        {
            return _context.Quotes.OrderBy(q => q.Id).ToList();
        }
        public ICollection<Quote> GetFavouriteQuotes()
        {
            return _context.Quotes.Where(q => q.IsFavourite == true).ToList();
        }

    }
}
