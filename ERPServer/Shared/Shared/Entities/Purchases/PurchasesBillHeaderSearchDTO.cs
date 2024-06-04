using Shared.Entities.Shared;
using System;

namespace Data.Entities.Purchases
{
    public class PurchasesBillHeaderSearchDTO : Paging
    {
        public string Number { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Notes { get; set; }
        public long? ClientVendorId { get; set; }
        public bool? IsTemp { get; set; }
        public bool? IsReturned { get; set; }
        public string PersonPhoneNumber { get; set; }
        public long? RepresentiveId { get; set; }
    }
}
