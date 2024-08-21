using BookManagerASP.Models;
using BookManagerASP.Queries;
using Microsoft.AspNetCore.Identity;

namespace BookManagerASP.Interfaces
{
    public interface IUserEntityRepository
    {
        Task<UserEntity> GetUser(UserEntityQuery query);

        Task<IdentityResult> CreateUserAsync(UserEntity user, string password);

        Task<bool> UserExistsAsync(UserEntityQuery query);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);
    }
}
