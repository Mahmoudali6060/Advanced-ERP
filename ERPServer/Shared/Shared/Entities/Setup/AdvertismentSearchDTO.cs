using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Setup
{
    public class AdvertismentSearchDTO : Paging
    {
        public string Media { get; set; }
        public string MediaBase64 { get; set; }
    }
}
