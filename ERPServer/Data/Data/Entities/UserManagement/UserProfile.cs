using Data.Entities.Setup;
using Data.Entities.Shared;


namespace Data.Entities.UserManagement
{
    public class UserProfile : BaseEntity
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool IsHide { get; set; }
        public string ImageUrl { get; set; }
        public UserTypeEnum UserTypeId { get; set; }
        public string Role { get; set; }
        public string DefaultLanguage { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public  long? CompanyId { get; set; }
        public virtual Company Company { get; set; }



    }
}
