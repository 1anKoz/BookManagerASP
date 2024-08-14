using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IQuoteRepository
    {
        Quote GetQuote(int quoteId);
        ICollection<Quote> GetQuotes();
        ICollection<Quote> GetFavouriteQuotes();
        bool QuoteExists(int quoteId);

        bool CreateQuote(Quote quote);
        bool Save();
    }
}
