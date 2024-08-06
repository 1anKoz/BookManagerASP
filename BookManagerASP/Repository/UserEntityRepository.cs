using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookManagerASP.Repository
{
    public class UserEntityRepository : IUserEntityRepository
    {
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
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
