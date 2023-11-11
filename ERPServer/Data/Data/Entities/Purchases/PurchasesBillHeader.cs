using Data.Entities.Setup;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Purchases
{
    public class PurchasesBillHeader : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Transfer { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaining { get; set; }
        public string Notes { get; set; }
        public long ClientVendorId { get; set; }
        public virtual ClientVendor ClientVendor { get; set; }
        public virtual List<PurchasesBillDetail> PurchasesBillDetailList { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
