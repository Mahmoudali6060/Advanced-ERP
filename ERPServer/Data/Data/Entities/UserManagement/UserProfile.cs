using Data.Entities.Setup;
using Data.Entities.Shared;
using NLog.Fluent;

namespace Data.Entities.UserManagement
{
    public class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool IsHide { get; set; }
        public string ImageUrl { get; set; }
        public long? RoleId { get; set; }
        public string DefaultLanguage { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public  long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual RoleGroup Role { get; set; }




    }
}
