using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Entities
{
    public class ResetPasswordDTO
    {

        [Required(ErrorMessage = "Errors.PasswordIsRequired")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Errors.InvalidPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Errors.ConfirmPasswordIsRequired")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Errors.InvalidConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
