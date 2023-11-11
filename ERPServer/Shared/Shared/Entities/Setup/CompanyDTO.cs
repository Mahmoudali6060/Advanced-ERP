using Entities.Account;
using Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Shared.Entities.Setup
{
    public class CompanyDTO 
    {
        public long Id { get; set; }
        public bool? IsActive { get; set; }
        public string ImageUrl { get; set; }
        public string ImageBase64 { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
        public string WebsiteLink { get; set; }
        public string AddressDetails { get; set; }

    }
}
