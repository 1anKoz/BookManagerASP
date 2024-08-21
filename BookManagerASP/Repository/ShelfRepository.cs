using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Queries;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookManagerASP.Repository
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly DataContext _context;

        public ShelfRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> ShelfExistsAsync(ShelfQuery query)
        {
            if (query.Name != null)
                return await _context.Shelves.AnyAsync(s => s.Name == query.Name);
            else if (query.Id != null)
                return await _context.Shelves.AnyAsync(s => s.Id == query.Id);
            else if (query.UserEntityId != null)
                return await _context.Shelves.AnyAsync(s => s.UserEntityId == query.UserEntityId);

            return false;
        }


        public async Task<Shelf> GetShelfAsync(ShelfQuery query)
        {
            if (query.Name != null && query.UserEntityId != null)
                return await _context.Shelves
                    .Where(s => s.Name == query.Name && s.UserEntityId == query.UserEntityId)
                    .FirstOrDefaultAsync();
            else if (query.Id != null && query.UserEntityId != null)
                return await _context.Shelves
                    .Where(s => s.Id == query.Id && s.UserEntityId == query.UserEntityId)
                    .FirstOrDefaultAsync();

            return await _context.Shelves.FirstOrDefaultAsync();
        }

        public async Task<ICollection<Shelf>> GetShelvesAsync(string userId)
        {
            return await _context.Shelves.Where(s => s.UserEntityId == userId).ToListAsync();
        }


        public async Task<bool> CreateShelfAsync(Shelf shelf)
        {
            await _context.AddAsync(shelf);
            return await SaveAsync();
        }

        public async Task<bool> UpdateShelfAsync(Shelf shelf)
        {
            _context.Update(shelf);
            return await SaveAsync();
        }
    }
}
