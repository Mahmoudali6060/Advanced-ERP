using Entities.Account;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Account.DataServiceLayer
{
    public interface IAccountDSL
    {
        Task<UserProfileDTO> Register(RegisterRequestDTO model);
        Task<UserProfileDTO> Login(LoginModel user);
        Task<bool> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordModel);
        Task<bool> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto);

    }
}
