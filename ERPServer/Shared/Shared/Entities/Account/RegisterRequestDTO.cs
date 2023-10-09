using Shared.Entities.Setup;
using System.ComponentModel.DataAnnotations;


namespace Entities.Account
{
    public class RegisterRequestDTO
    {
        public RegisterDTO RegisterDTO { get; set; }
        public CompanyDTO CompanyDTO { get; set; }
    }
}
