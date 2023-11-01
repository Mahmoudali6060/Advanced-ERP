using Data.Constants;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Entities.Account;
using Shared.Entities.Setup;
using System;
using System.ComponentModel;

namespace Account.Helpers
{
    public class UserMapper
    {

        public static AppUser MapAppUser(RegisterDTO model)
        {
            return new AppUser
            {
                UserName = model.Username,
                Email = model.Email
            };
        }

        public static AppUser MapAppUser(UserProfileDTO model)
        {
            return new AppUser
            {
                UserName = model.UserName,
                Email = string.IsNullOrWhiteSpace(model.Email) ? model.UserName : model.Email
            };
        }
        public static AppUser MapAppUser(CompanyDTO model)
        {
            return new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
        }
        public static Company MapCompany(UserProfileDTO model)
        {
            return new Company
            {
                Id = (long)model.CompanyId,
                AddressDetails = model.CompanyDTO.UserName,
                ContactPerson = model.CompanyDTO.ContactPerson,
                ContactTelephone = model.CompanyDTO.ContactTelephone,
            };
        }
        public static UserProfileDTO MapAppUser(AppUser appUser, UserProfile userProfile)
        {
            return new UserProfileDTO
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Mobile = userProfile.Mobile,
                Email = appUser.Email,
                UserName = appUser.UserName,
                DefaultLanguage = userProfile.DefaultLanguage,
                RoleId = userProfile.RoleId,
                RoleName = userProfile.Role.Name,
                AppUserId = appUser.Id,
                ImageUrl = userProfile.ImageUrl,
              
                IsActive = userProfile.IsActive,
                IsFirstLogin = userProfile.IsFirstLogin,
                CompanyId = userProfile?.CompanyId,
                IsHide = userProfile.IsHide

            };
        }

        public static UserProfileDTO MapRegisterRequestViewModel(RegisterRequestDTO registerRequestViewModel)
        {
            return new UserProfileDTO
            {
                UserName = registerRequestViewModel.RegisterDTO.Username,
                Email = registerRequestViewModel.RegisterDTO.Email,
                Password = registerRequestViewModel.RegisterDTO.Password,
            };
        }

        private static string GetUserRole(UserTypeEnum userTypeId)
        {
            switch (userTypeId)
            {
                case UserTypeEnum.Admin:
                    return "Amin";
                case UserTypeEnum.Employee:
                    return "Employee";
                default:
                    return "Employee";
            }
        }

    }
}
