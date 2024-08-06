using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(int id);
        Book GetBook(string title = null, string author = null);
        int GetBookRating(int id);
        bool BookExists(int bookId);
    }
}
