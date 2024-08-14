using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;

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

        public bool UserExists(string userNameOrEmail)
        {
            if (userNameOrEmail.Contains("@"))
            {
                return _userManager.Users.Any(u => u.Email == userNameOrEmail);
            }

            return _userManager.Users.Any(u => u.UserName == userNameOrEmail);
        }

        public async Task<UserEntity> GetUser(string userNameOrEmail)
        {
            if (userNameOrEmail.Contains("@"))
            {
                return await _userManager.FindByEmailAsync(userNameOrEmail);
            }

            return _userManager.Users.Where(un => un.UserName == userNameOrEmail).FirstOrDefault();
        }


        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

    }
}
