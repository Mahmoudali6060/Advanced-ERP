using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Account.Entities
{
    public class UserProfileDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Errors.FirstNameIsRequired")]
        [StringLength(50, ErrorMessage = "Errors.InvalidFirstName", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Errors.LastNameIsRequired")]
        [StringLength(50, ErrorMessage = "Errors.InvalidLastName", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Errors.MobileIsRequired")]
        //[StringLength(50, ErrorMessage = "Errors.InvalidMobile", MinimumLength = 2)]
        [RegularExpression("^(01)[0-2,5]{1}[0-9]{8}", ErrorMessage = "Errors.InvalidMobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Errors.JobTitleIsRequired")]
        [StringLength(50, ErrorMessage = "Errors.InvalidJobTitle", MinimumLength = 2)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Errors.EmailIsRequired")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Errors.InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Errors.UserNameIsRequired")]
        [StringLength(50, ErrorMessage = "Errors.InvalidUserName", MinimumLength = 2)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Errors.InvalidPassword")]
        public string Password { get; set; }
        public string DefaultLanguage { get; set; }

        [Required( ErrorMessage = "Errors.InvalidRole")]
        [StringLength(50, MinimumLength = 2)]
        public string Role { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public string AppUserId { get; set; }
        public string ImageBase64 { get; set; }
        public string ImageUrl { get; set; }
        public string DeviceId { get; set; }



    }
}
