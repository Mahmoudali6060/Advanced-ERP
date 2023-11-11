using Data.Entities.Setup;
using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.UserManagement
{
    public class RoleGroup :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<RolePrivilege> RolePrivileges { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
