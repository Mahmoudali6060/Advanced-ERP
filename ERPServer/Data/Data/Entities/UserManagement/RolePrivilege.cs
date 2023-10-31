using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.UserManagement
{
    public class RolePrivilege :BaseEntity
    {
       
        public long PrivilegeId { get; set; }
        public long RoleGroupId { get; set; }
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
