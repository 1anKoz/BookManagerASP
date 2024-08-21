using BookManagerASP.Data.Enum;
using BookManagerASP.Models;
using BookManagerASP.Queries;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        Task <Book> GetBookAsync(BookQuery query);
        Task<ICollection<Book>> GetBooksAsync(BookQuery query);

        Task<bool> BookExistsAsync(BookQuery query);

        Task<bool> UpdateBookAsync(Book book);

        Task<bool> CreateBookAsync(Book book);
        Task<bool> SaveAsync();
    }
}
