using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.Account
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
    }
}
