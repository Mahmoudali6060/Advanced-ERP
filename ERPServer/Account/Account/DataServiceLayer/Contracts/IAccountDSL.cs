using Account.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.DataServiceLayer
{
    public interface IAccountDSL
    {
        Task<UserProfileDTO> Register(RegisterRequestViewModel model);
        Task<UserProfileDTO> Login(LoginModel user);
        Task<bool> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordModel);
        Task<bool> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto);

    }
}
