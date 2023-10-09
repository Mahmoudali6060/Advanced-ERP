using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.UserManagement
{
    public class Privilege : BaseEntity
    {
        public string Name { get; set; }
    }
}
