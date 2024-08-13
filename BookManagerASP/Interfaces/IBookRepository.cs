using BookManagerASP.Data.Enum;
using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        ICollection<Book> GetBooksByAuthor(string author);
        Book GetBook(int id);
        Book GetBook(string title = null);
        //ICollection<Book> GetBooksByGenre(Genre genre);
        int GetBookRating(int id);
        bool BookExists(int bookId);

        bool CreateBook(Book book);
        bool Save();
    }
}
