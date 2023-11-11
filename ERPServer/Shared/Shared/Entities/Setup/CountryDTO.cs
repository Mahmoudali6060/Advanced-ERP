using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class CountryDTO :BaseDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
