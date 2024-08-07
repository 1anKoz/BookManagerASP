using BookManagerASP.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManagerASP.Interfaces
{
    public interface IUserEntityRepository
    {
        Task<UserEntity> GetUser(string parameter);

        Task<IdentityResult> CreateUserAsync(UserEntity user, string password);

        public bool UserExists(string email);
    }
}
