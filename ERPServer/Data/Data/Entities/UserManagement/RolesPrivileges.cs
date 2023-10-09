using Data.Entities.Shared;


namespace Data.Entities.UserManagement
{
    public class RolesPrivileges : BaseEntity
    {
        public long RoleId { get; set; }
        public string PrivilegeId { get; set; }
    }
}
