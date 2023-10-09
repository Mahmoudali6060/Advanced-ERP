using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.UserManagement
{
    public class UserDevice : BaseEntity
    {
        public string DeviceId { get; set; }
        public long UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
