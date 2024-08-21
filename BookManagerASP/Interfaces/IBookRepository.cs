using BookManagerASP.Data.Enum;
using BookManagerASP.Models;
using BookManagerASP.Queries;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        Task<bool> SaveAsync();
        Task<bool> BookExistsAsync(BookQuery query);

        Task <Book> GetBookAsync(BookQuery query);
        Task<ICollection<Book>> GetBooksAsync(BookQuery query);


        Task<bool> UpdateBookAsync(Book book);

        Task<bool> CreateBookAsync(Book book);
    }
}
