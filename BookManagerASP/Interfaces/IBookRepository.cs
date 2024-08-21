using BookManagerASP.Data.Enum;
using BookManagerASP.Models;
using BookManagerASP.Queries;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(BookQuery query);
        ICollection<Book> GetBooks(BookQuery query);

        bool BookExists(BookQuery query);

        bool UpdateBook (Book book);

        bool CreateBook(Book book);
        bool Save();
    }
}
