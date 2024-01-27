using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;

namespace Data.Entities.Setup
{
    public class Company : IEntity
    {
        public long Id { get; set; }
        public bool? IsActive { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string AddressDetails { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
        public string WebsiteLink { get; set; }
        public Setting Setting { get; set; }


    }
}
