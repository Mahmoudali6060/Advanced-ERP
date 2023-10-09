using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Account.Entities
{
    public class ForgotPasswordDTO
    {

        [Required(ErrorMessage = "Errors.EmailIsRequired")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Errors.InvalidEmail")]
        public string Email { get; set; }
        [Required]
        public string ClientURI { get; set; }
    }
}
