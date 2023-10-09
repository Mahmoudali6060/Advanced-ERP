using Entities.Account;
using Entities.Shared;

using Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Shared.Entities.Setup
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        public string LogoURL { get; set; }
        public string LogoBase64 { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
        public string WebsiteLink { get; set; }
        public string AddressDetails { get; set; }

        #region for profile and aspnet users
        //[Required(ErrorMessage = "Errors.EmailIsRequired")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Errors.InvalidEmail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Errors.UserNameIsRequired")]
        [StringLength(50, ErrorMessage = "Errors.InvalidUserName", MinimumLength = 2)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Errors.InvalidPassword")]
        public string Password { get; set; }
        public string Role { get; set; }
        public int UserTypeId { get; set; }

        #endregion
    }
}
