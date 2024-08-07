using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookManagerASP.Repository
{
    public class UserEntityRepository : IUserEntityRepository
    {
        protected ILookupNormalizer normalizer;

        private readonly UserManager<UserEntity> _userManager;

        public UserEntityRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public bool UserExists(string email)
        {
            return _userManager.Users.Any(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Log or debug here
                Console.WriteLine($"User with email {email} not found.");
            }
            else
            {
                // Log or debug here
                Console.WriteLine($"User with email {email} found: {user.UserName}");
            }
            return user;
        }

        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
