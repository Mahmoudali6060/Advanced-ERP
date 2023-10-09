using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class CountrySearchDTO : Paging
    {
        public string Name { get; set; }
    }
}
