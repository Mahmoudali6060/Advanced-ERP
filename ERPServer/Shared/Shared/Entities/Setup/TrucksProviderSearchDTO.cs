using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Setup
{
    public class TrucksProviderSearchDTO : Paging
    {
        public string ProviderNameEn { get; set; }
        public string ProviderNameAr { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public string AddressDetails { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTelephone { get; set; }
    }
}
