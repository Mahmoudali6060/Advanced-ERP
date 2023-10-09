
using Data.Entities.Shared;
using Data.Entities.UserManagement;

namespace Account.Entities
{
    public class RegisterResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public RegisterResponseViewModel(AppUser user)
        {
            //Id = user.Id;
            //Name = user.FirstName + user.LastName;
            //Email = user.Email;
        }
    }
}
