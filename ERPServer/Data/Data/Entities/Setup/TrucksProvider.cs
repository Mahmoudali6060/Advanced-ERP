using Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class TrucksProvider : BaseEntity
    {
        public string ProviderLogo { get; set; }
        public string ProviderNameEn { get; set; }
        public string ProviderNameAr { get; set; }
        //Address
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
        public long StateId { get; set; }
        public virtual State State { get; set; }
        public long? CityId { get; set; }
        public virtual City City { get; set; }
        public string WebsiteLink { get; set; }
        public string AddressDetails { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }


    }
}
