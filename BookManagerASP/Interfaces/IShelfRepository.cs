using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IShelfRepository
    {
        bool ShelfExists(int shelfId);

        ICollection<Shelf> GetAllShelves();
        Shelf GetShelf(int shelfId);
        ICollection<Shelf> GetUserShelves(string userId);
    }
}
