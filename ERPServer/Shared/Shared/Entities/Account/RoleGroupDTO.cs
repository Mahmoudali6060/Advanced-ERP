using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Account
{
    public class RoleGroupDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<RolePrivilegeDTO> RolePrivileges { get; set; }
    }
}
