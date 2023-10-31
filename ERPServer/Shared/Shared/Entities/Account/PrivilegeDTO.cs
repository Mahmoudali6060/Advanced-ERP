using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Account
{
    public class RolePrivilegeDTO
    {
        public long RoleGroupId { get; set; }
        public long PrivilegeId { get; set; }
    }
}
