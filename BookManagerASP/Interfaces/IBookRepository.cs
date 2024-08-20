using BookManagerASP.Data.Enum;
using BookManagerASP.Models;
using BookManagerASP.Queries;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(BookQuery query);
        ICollection<Book> GetBooks(BookQuery query);
        //ICollection<Book> GetBooksByAuthor(string author);
        //Book GetBook(string title = null);
        ////ICollection<Book> GetBooksByGenre(Genre genre);
        //int GetBookRating(int id);
        bool BookExists(BookQuery query);

        bool UpdateBook (Book book);

        bool CreateBook(Book book);
        bool Save();
    }
}
