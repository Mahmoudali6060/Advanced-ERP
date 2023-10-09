using Account.DataAccessLayer;
using Account.DataServiceLayer.Contracts;
using Account.Entities;
using Account.RepositoryLayer;
using Accout.DataServiceLayer;
using Data.Constants;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Setting.DataServiceLayer;
using Shared.Entities;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.DataServiceLayer
{
    public class AccountDSL : IAccountDSL
    {
        IAccountDAL _accountDAL;
        ISettingDSL _settingDSL;
        IUserProfileDSL _userProfileDSL;
        IUserDeviceDSL _userDeviceDSL;
        ApplicationSettings _appSettings;
        private readonly IEmailSender _emailSender;

        public AccountDSL(IAccountDAL accountDAL, ISettingDSL settingDSL, IOptions<ApplicationSettings> appSettings, IUserProfileDSL userProfileDSL, IUserDeviceDSL userDeviceDSL, ILoggerManager logger, IEmailSender emailSender)
        {
            _accountDAL = accountDAL;
            _settingDSL = settingDSL;
            _appSettings = appSettings.Value;
            _userProfileDSL = userProfileDSL;
            _emailSender = emailSender;
            _userDeviceDSL = userDeviceDSL;
        }

        public async Task<UserProfileDTO> Register(RegisterRequestViewModel model)
        {
            UserProfileDTO userDto = UserMapper.MapRegisterRequestViewModel(model);
            var result = await _userProfileDSL.Add(userDto);
            if (result > 0)
            {
                userDto.Id = result;
                userDto.Token = AddToken();//Generate Token for this user
                return userDto;
            }

            return null;
        }

        public async Task<UserProfileDTO> Login(LoginModel loginModel)
        {
            var result = await _accountDAL.IsValidUser(loginModel);//Check Email-UserName-Phone Number 
            if (result.Succeeded)
            {
                var appUser = await _accountDAL.GetUserByUsername(loginModel);
                var userProfile = await _userProfileDSL.GetUserProfileByAppUserId(appUser.Id);
                if (userProfile == null)
                    throw new Exception("Errors.InvalidUsernameOrPassword");
                UserProfileDTO userDto = UserMapper.MapAppUser(appUser, userProfile);

                var role = await _accountDAL.GetRolesAsync(appUser);
                userDto.Token = AddToken(appUser, role);
                userDto.Email = appUser.Email;
                // "cfz2ZzlZQ5eu2AMZNxbHlQ:APA91bHZySyNQP9uO9xxNaj9UHgiNRxGOIKZ50tQ3xhjW8KsyFJoc1Zw53twZ62qRqAU4iPML3lSa6fQqCxc-rW6T9WgyR4iVvFk35lqXZfRxpq-3NZ3k0LSNqmI5UUoeTJIXd90o9VI";
                //Set DeviceId for driver and Update it when user login
                if (!string.IsNullOrWhiteSpace(loginModel.DeviceId))
                {
                    userDto.DeviceId = loginModel.DeviceId;
                    await _userDeviceDSL.SetDeviceId(userDto);
                }
                return userDto;
            }
            throw new Exception("Errors.InvalidUsernameOrPassword");
        }

        private string AddToken(AppUser appUser, IList<string> role)
        {
            IdentityOptions _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim("UserID",appUser.Id.ToString()),
                            new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private bool IsValidClient()
        {
            string curFile = @"C:\users\doc.txt";
            List<Settings> settings = _settingDSL.GetSettings();
            if (File.Exists(curFile) && settings[0].ExpiryDate.Date > DateTime.Now.Date)
            {
                return true;
            }
            throw new Exception("Error.NotAccessed");
        }

        public string AddToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:54095",
                audience: "http://localhost:54095",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public async Task<bool> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordModel)
        {
            var user = await _accountDAL.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                throw new Exception("Errors.InvalidEmail");

            var token = await _accountDAL.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
             {
                 {"token", token },
                 {"email", forgotPasswordModel.Email }
             };

            var callback = QueryHelpers.AddQueryString(forgotPasswordModel.ClientURI, param);
            var hash = callback.Split("#");
            var query = hash[0];
            string replace = query.Replace("/?", "/#/resetPassword?");
            var message = new MessageDTO(new string[] { user.Email }, "UN.", $"Dear {user.UserName}\r\n Please follow link to reset your password {replace}");
            _emailSender.SendEmail(message);
            return true;
        }

        public async Task<bool> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            var user = await _accountDAL.FindByEmailAsync(resetPasswordDto.Email);
            resetPasswordDto.Token = await _accountDAL.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _accountDAL.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                throw new Exception("Errors Invalid Data");
            }
            return true;

        }

    }
}
