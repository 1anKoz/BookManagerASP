using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        public async Task<bool> UserExistsAsync(UserEntityQuery query)
        {
            if (query.UserName != null)
                return await _userManager.Users.AnyAsync(u => u.UserName == query.UserName);
            else if (query.Email != null)
                return await _userManager.Users.AnyAsync(u => u.Email == query.Email);
            else if(query.Id != null)
                return await _userManager.Users.AnyAsync(u => u.Id == query.Id);

            return false;
        }

        public async Task<UserEntity> GetUser(UserEntityQuery query)
        {
            if (query.UserName != null)
                return await _userManager.Users.Where(u => u.UserName == query.UserName)
                    .FirstOrDefaultAsync();
            else if (query.Email != null)
                return await _userManager.Users.Where(u => u.Email == query.Email)
                    .FirstOrDefaultAsync();
            else
                return await _userManager.Users.Where(u => u.Id == query.Id)
                    .FirstOrDefaultAsync();
        }


        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
