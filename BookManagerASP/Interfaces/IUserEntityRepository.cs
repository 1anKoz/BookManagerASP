using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManagerASP.Interfaces
{
    public interface IUserEntityRepository
    {
        Task<UserEntity> GetUser(string userNameOrEmail);

        Task<IdentityResult> CreateUserAsync(UserEntity user, string password);

        public bool UserExists(string userNameOrEmail);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);
    }
}
