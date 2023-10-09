using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Account
{
    public class UserProfileSearchCriteriaDTO : Paging
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
        public bool? IsActive { get; set; }





    }
}
