using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManagerASP.Repository
{
    public class UserEntityRepository : IUserEntityRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserEntityRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
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
