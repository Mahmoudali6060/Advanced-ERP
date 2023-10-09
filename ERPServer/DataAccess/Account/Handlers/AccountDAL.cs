using Data.Contexts;
using Data.Entities.UserManagement;
using Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public class AccountDAL : IAccountDAL
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        public AccountDAL(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext appDbContext,RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
        }


        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password, string role)
        {
            var IsRoleExist = await _roleManager.RoleExistsAsync(role);
            if(IsRoleExist)
            {
                await ValidateEmailAndUserName(user);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return await AddToRoleAsync(user, role);

                }
                return result;
            }
            else
            {
                throw new Exception("Errors.NonExistRole");
            }

        }
        private async Task<bool> ValidateEmailAndUserName(AppUser user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser != null)
            {
                if (appUser.UserName == user.UserName)
                {
                    throw new Exception("Errors.UserNameExist");
                }
                else if (appUser.Email == user.Email)
                {
                    throw new Exception("Errors.EmailExist");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public async Task<IdentityResult> UpdateUserAsync(UserProfileDTO userProfileDTO)
        {
            var appUser = await _userManager.FindByIdAsync(userProfileDTO.AppUserId);
            var IsUserNameExist = await _userManager.Users.AnyAsync(x => x.UserName == userProfileDTO.UserName && x.Id != appUser.Id);
            if (IsUserNameExist)
            {
                throw new Exception("Errors.UserNameExist");
            }
            var IsEmailExist = await _userManager.Users.AnyAsync(x => x.Email == userProfileDTO.Email && x.Id != appUser.Id);
            if (IsEmailExist)
            {
                throw new Exception("Errors.EmailExist");
            }
            appUser.Email = userProfileDTO.Email;
            appUser.UserName = userProfileDTO.UserName;
            var result = await _userManager.UpdateAsync(appUser);   //CreateAsync(user, password);
            return result;
        }
        public async Task<SignInResult> IsValidUser(LoginModel loginModel)
        {
            return await _signInManager.PasswordSignInAsync(
             loginModel.UserName, loginModel.Password,
             isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<AppUser> GetUserByUsername(string UserName)
        {
            return await _userManager.FindByNameAsync(UserName);
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {

             return  await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<UserProfile> GetLastUserAsync()
        {
            return await _appDbContext.UserProfiles.SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(appUser);
            return true;
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult> ResetPasswordAsync(AppUser appUser, string token, string password)
        {
            var resetPassResult = await _userManager.ResetPasswordAsync(appUser, token, password);
            return resetPassResult;
        }
    }
}

