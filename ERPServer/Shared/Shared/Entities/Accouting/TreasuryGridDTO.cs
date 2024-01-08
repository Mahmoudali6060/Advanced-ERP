using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shared.Entities.Accouting
{
    public class TreasuryGridDTO
    {
        public IEnumerable<TreasuryDTO> List { get; set; }
        public int Total { get; set; }
        public decimal Balance { get; set; }

    }
}
