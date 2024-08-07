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

        public bool UserExists(string parameter)
        {
            if (parameter.Contains("@"))
            {
                return _userManager.Users.Any(u => u.Email == parameter);
            }

            return _userManager.Users.Any(u => u.UserName == parameter);
        }

        public async Task<UserEntity> GetUser(string parameter)
        {
            if (parameter.Contains("@"))
            {
                return await _userManager.FindByEmailAsync(parameter);
            }

            return _userManager.Users.Where(un => un.UserName == parameter).FirstOrDefault();
        }


        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

    }
}
