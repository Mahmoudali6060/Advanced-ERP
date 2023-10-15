using Shared.Entities.Shared;
using System;

namespace Data.Entities.Purchases
{
    public class PurchasesBillHeaderSearchDTO  : Paging
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public long? VendorId { get; set; }

    }
}
