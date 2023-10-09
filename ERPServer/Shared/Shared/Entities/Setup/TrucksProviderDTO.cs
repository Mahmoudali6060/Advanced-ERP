using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Setup
{
    public class TrucksProviderDTO
    {
        public long Id { get; set; }
        public string ProviderLogo { get; set; }
        public string ProviderLogoBase64 { get; set; }
        public string ProviderNameEn { get; set; }
        public string ProviderNameAr { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public long StateId { get; set; }
        public string StateName { get; set; }
        public string WebsiteLink { get; set; }
        public string AddressDetails { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
    }
}
