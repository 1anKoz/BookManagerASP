using BookManagerASP.Dto;
using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IQuoteRepository
    {
        Quote GetQuote(int? quoteId);
        ICollection<Quote> GetQuotes();
        ICollection<Quote> GetFavouriteQuotes();
        ICollection<Quote> GetUserQuotes(string userId);
        ICollection<Quote> GetBookPrivateQuotes(int bookPrivateId);
        bool QuoteExists(int quoteId);

        bool CreateQuote(Quote quote);
        bool Save();

        bool UpdateQuote(Quote quote);
    }
}
