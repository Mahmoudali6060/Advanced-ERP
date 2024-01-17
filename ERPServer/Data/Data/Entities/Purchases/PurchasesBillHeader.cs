using Data.Entities.Accouting;
using Data.Entities.Setup;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Purchases
{
    [Table(nameof(PurchasesBillHeader))]
    public class PurchasesBillHeader : AuditEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAfterVAT { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaining { get; set; }
        public bool IsTax { get; set; }
        public bool IsTemp { get; set; }
        public bool IsCancel { get; set; }
        public bool IsReturned { get; set; }
        public string Notes { get; set; }
        public long ClientVendorId { get; set; }
        public long? RepresentiveId { get; set; }
        public long? ParentId { get; set; }

        public virtual ClientVendor ClientVendor { get; set; }

        public virtual List<PurchasesBillDetail> PurchasesBillDetailList { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public long? AccountStatementId { get; set; }
        public virtual AccountStatement AccountStatement { get; set; }
        public long? TreasuryId { get; set; }
        public virtual Treasury Treasury { get; set; }
    }
}
