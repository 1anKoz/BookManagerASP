using BookManagerASP.Models;
using BookManagerASP.Queries;

namespace BookManagerASP.Interfaces
{
    public interface IShelfRepository
    {
        Task<bool> SaveAsync();
        Task<bool> ShelfExistsAsync(ShelfQuery query);

        Task<Shelf> GetShelfAsync(ShelfQuery query);
        Task<ICollection<Shelf>> GetShelvesAsync(string userId);

        Task<bool> CreateShelfAsync(Shelf shelf);

        Task<bool> UpdateShelfAsync(Shelf shelf);
    }
}
