using System.ComponentModel.DataAnnotations;


namespace Account.Entities
{
    public class RegisterRequestViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string JobTitle { get; set; }

        //[Required]
        [EmailAddress(ErrorMessage ="Invlid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "Default Language")]
        public string DefaultLanguage { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
