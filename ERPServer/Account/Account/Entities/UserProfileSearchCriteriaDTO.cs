using Data.Entities.UserManagement;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Account.Entities
{
    public class UserProfileSearchCriteriaDTO : DataSource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserTypeEnum UserTypeId { get; set; }
        //public string DefaultLanguage { get; set; }
        //public string Role { get; set; }

    }
}
