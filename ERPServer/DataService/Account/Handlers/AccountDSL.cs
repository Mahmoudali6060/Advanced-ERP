using Account.DataServiceLayer.Contracts;
using Account.Helpers;
using AutoMapper;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Entities.Account;
using IdentityModel;
using Infrastructure.Contracts;
using Infrastructure.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Classes;
using Shared.Entities.Setup;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Account.DataServiceLayer
{
    public class AccountDSL : IAccountDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        private readonly IConfiguration _configuration;
        public AccountDSL(IUnitOfWork unitOfWork, IFileManager fileManager, UserManager<AppUser> userManager, IMapper mapper, IEmailSender emailSender, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _fileManager = fileManager;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserProfileDTO> Register(RegisterRequestDTO model)
        {
            UserProfileDTO userDto = UserMapper.MapRegisterRequestViewModel(model);

            #region Create AspNetUser
            AppUser appUser = UserMapper.MapAppUser(userDto);
            var createUserResult = await _unitOfWork.AccountDAL.CreateUserAsync(appUser, userDto.Password);
            #endregion


            #region Create  UserProfile
            if (createUserResult.Succeeded == true)
            {
                userDto.AppUserId = appUser.Id;
                userDto.DefaultLanguage = "en";
                userDto.CompanyId = model.CompanyDTO.Id > 0 ? model.CompanyDTO.Id : null;
                userDto.IsActive = true;
                userDto.IsFirstLogin = false;
                userDto.IsHide = false;
                //UploadImage(entity);
                long userProfileIdResult = await _unitOfWork.UserProfileDAL.Add(_mapper.Map<UserProfile>(userDto));
                //Create Token
                if (userProfileIdResult > 0)
                {
                    userDto.Id = userProfileIdResult;

                    userDto.Token = AddToken();//Generate Token for this user
                }
                else
                {
                    await _unitOfWork.AccountDAL.DeleteUser(appUser.Id);
                }
            }
            #endregion

            await _unitOfWork.CompleteAsync();

            return userDto;

        }

        //private void UploadAllDocuments(CompanyDTO model)
        //{
        //    if (model.CompanyLogoBase64 != null)
        //    {
        //        model.CompanyLogoURL = DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + "_" + model.CompanyLogoURL;
        //        UploadImage(model.CompanyLogoURL, model.CompanyLogoBase64);
        //    }

        //    if (model.CommercialRegistration?.FileBase64 != null)
        //    {
        //        model.CommercialRegistration.Url = DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + "_" + model.CommercialRegistration.Url;
        //        UploadFile(model.CommercialRegistration.Url, model.CommercialRegistration.FileBase64);
        //    }

        //    if (model.TaxId?.FileBase64 != null)
        //    {
        //        model.TaxId.Url = DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + "_" + model.TaxId.Url;
        //        UploadFile(model.TaxId.Url, model.TaxId.FileBase64);
        //    }

        //    if (model.VatId?.FileBase64 != null)
        //    {
        //        model.VatId.Url = DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + "_" + model.VatId.Url;
        //        UploadFile(model.VatId.Url, model.VatId.FileBase64);
        //    }

        //    if (model.IndustrialRegistration?.FileBase64 != null)
        //    {
        //        model.IndustrialRegistration.Url = DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + "_" + model.IndustrialRegistration.Url;
        //        UploadFile(model.IndustrialRegistration.Url, model.IndustrialRegistration.FileBase64);
        //    }
        //}

        private bool UploadFile(string fileName, string fileBase64)
        {
            if (fileBase64 != null)
            {
                //entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.UserName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Documents/RegistrationRequests/" + fileName, fileBase64);
            }
            return true;
        }

        private bool UploadImage(string fileName, string fileBase64)
        {
            if (fileBase64 != null)
            {
                //entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.UserName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/RegistrationRequests/" + fileName, fileBase64);
            }
            return true;
        }
        public async Task<UserProfileDTO> Login(LoginModel loginModel)
        {
            var result = await _unitOfWork.AccountDAL.IsValidUser(loginModel);//Check Email-UserName-Phone Number 
            if (result.Succeeded)
            {
                var appUser = await _unitOfWork.AccountDAL.GetUserByUsername(loginModel.UserName);
                var userProfile = await _unitOfWork.UserProfileDAL.GetUserProfileByAppUserId(appUser.Id);
                if (userProfile.IsActive == false)
                {
                    throw new Exception("Errors.contactAdmin");
                }
                if (userProfile == null)
                    throw new Exception("Errors.InvalidUsernameOrPassword");
                UserProfileDTO userDto=_mapper.Map<UserProfileDTO>(userProfile);
                userDto.Token = AddToken(appUser, userDto);
                userDto.Email = appUser.Email;
                userDto.UserName = appUser.UserName;
                userDto.AppUserId = appUser.Id;

                return userDto;
            }
            throw new Exception("Errors.InvalidUsernameOrPassword");
        }
        public async Task<bool> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordModel)
        {
            var user = await _unitOfWork.AccountDAL.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                throw new Exception("Errors.InvalidEmail");

            var token = await _unitOfWork.AccountDAL.GeneratePasswordResetTokenAsync(user);
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
            // if we have not email , username = email 
            var user = await _unitOfWork.AccountDAL.FindByEmailAsync(resetPasswordDto.Email);
            resetPasswordDto.Token = await _unitOfWork.AccountDAL.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _unitOfWork.AccountDAL.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                throw new Exception("Errors Invalid Data");
            }
            else
            {
                var userProfile = await _unitOfWork.UserProfileDAL.GetUserProfileByAppUserId(user.Id);
                userProfile.IsFirstLogin = false;
                await _unitOfWork.UserProfileDAL.Update(userProfile);
            }
            return true;

        }

        #region Helper Methods
        private string AddToken(AppUser appUser, UserProfileDTO userProfileDTO)
        {
            IdentityOptions _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim("AppUserId",appUser.Id.ToString()),
                            new Claim(ClaimTypes.NameIdentifier,userProfileDTO.Id.ToString()),
                            new Claim(_options.ClaimsIdentity.RoleClaimType,userProfileDTO.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            //_userManager.AddClaimAsync(appUser, new Claim("UserProfileId", userProfileDTO.Id.ToString()));
            return tokenHandler.WriteToken(securityToken);
        }

        private string AddToken()
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

        private void SendEmail(long requestId, long userId, string requestNumber, string contactPerson, string email)
        {
            List<string> emails = new List<string>();
            emails.Add(email);
            string subject = "Created Request";
            string body = GenerateMessageBody(requestId, userId, requestNumber, contactPerson);
            MessageDTO messageDTO = new MessageDTO(emails, subject, body);
            _emailSender.SendEmail(messageDTO);
        }

        private string GenerateMessageBody(long requestId, long userId, string requestNumber, string contactPerson)
        {
            var Url = _configuration.GetSection(SystemKeys.AngularUrl).GetSection(SystemKeys.LocalHostUrl).Value;
            return "Dear " + contactPerson + "," + "\n\t" + "You have already registered the request number:" + requestNumber + ",so you can visit the following link to follow up: \n\t" + Url + "request-following/" + requestId + "/" + userId;
        }

        #endregion
    }
}
