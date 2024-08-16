using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
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
        public bool ShelfExists(int shelfId)
        {
            return _context.Books.Any(s => s.Id == shelfId);
        }


        public ICollection<Shelf> GetAllShelves()
        {
            return _context.Shelves.OrderBy(s => s.Id).ToList();
        }

        public Shelf GetShelf(int shelfId)
        {
            return _context.Shelves.Where(s => s.Id == shelfId).FirstOrDefault();
        }

        public ICollection<Shelf> GetUserShelves(string userId)
        {
            return _context.Shelves.Where(s => s.UserEntityId == userId).ToList();
        }


        public bool CreateShelf(Shelf shelf)
        {
            _context.Add(shelf);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
