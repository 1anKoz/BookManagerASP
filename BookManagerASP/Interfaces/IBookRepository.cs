using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks(); 
    }
}
