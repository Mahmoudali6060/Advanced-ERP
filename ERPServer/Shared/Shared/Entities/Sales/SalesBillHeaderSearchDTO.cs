using Shared.Entities.Shared;
using System;

namespace Data.Entities.Sales
{
    public class SalesBillHeaderSearchDTO  : Paging
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public long? ClientId { get; set; }

    }
}
