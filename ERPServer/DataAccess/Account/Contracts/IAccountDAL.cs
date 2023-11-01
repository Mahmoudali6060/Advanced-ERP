

using Data.Entities.UserManagement;
using Entities.Account;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public interface IAccountDAL
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> UpdateUserAsync(UserProfileDTO userProfileDTO);
        Task<SignInResult> IsValidUser(LoginModel loginModel);
        Task<AppUser> GetUserByUsername(string UserName);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<bool> DeleteUser(string userId);
        Task<AppUser> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(AppUser appUser);
        Task<IdentityResult> ResetPasswordAsync(AppUser appUser,string token, string password);

    }
}
