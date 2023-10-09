using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Account.DataServiceLayer;
using Entities.Account;


namespace App.Controllers.Account
{

    [Route("Api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        IAccountDSL _accountDSL;
        public AccountController(IAccountDSL accountDSL)
        {
            _accountDSL = accountDSL;

        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user) => Ok(await _accountDSL.Login(user));

        [HttpPost]
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model) => Ok(await _accountDSL.Register(model));
        [HttpPost]
        [HttpPost, Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model) => Ok(await _accountDSL.ForgotPassword(model));
        [HttpPost]
        [HttpPost, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model) => Ok(await _accountDSL.ResetPassword(model));
    }
}
