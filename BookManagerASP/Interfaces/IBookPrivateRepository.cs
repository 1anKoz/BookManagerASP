using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IBookPrivateRepository
    {
        bool BookPrivateExists(int? bookPrivateId);
        BookPrivate GetBookPrivate(int? bookPrivateId);
        BookPrivate GetBookPrivate(string title = null);
        ICollection<BookPrivate> GetAllBookPrivates();
        ICollection<BookPrivate> GetUserBookPrivates(string userId);
        ICollection<BookPrivate> GetBookPrivatesByBookAuthor(string author);
        ICollection<BookPrivate> GetFavouriteBookPrivates();
        ICollection<BookPrivate> GetBookPrivatesOnShelf(int shelfId);

        bool Save();
        bool CreateBookPrivate(BookPrivate bookPrivate);

        bool UpdateBookPrivate(int shelfId, BookPrivate bookPrivate);
    }
}
