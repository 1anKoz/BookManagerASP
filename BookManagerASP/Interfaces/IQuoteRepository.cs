using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IQuoteRepository
    {
        ICollection<Quote> GetQuotes();
        ICollection<Quote> GetFavouriteQuotes();
        //bool QuoteExists(int quoteId);
    }
}
