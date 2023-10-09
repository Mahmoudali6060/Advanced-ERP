

using Account.Entities;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public interface IAccountDAL
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password, string role);
        Task<IdentityResult> UpdateUserAsync(UserProfileDTO userProfileDTO);
        Task<SignInResult> IsValidUser(LoginModel loginModel);
        Task<AppUser> GetUserByUsername(LoginModel loginModel);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<bool> DeleteUser(string userId);
        Task<AppUser> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(AppUser appUser);
        Task<IdentityResult> ResetPasswordAsync(AppUser appUser,string token, string password);

    }
}
