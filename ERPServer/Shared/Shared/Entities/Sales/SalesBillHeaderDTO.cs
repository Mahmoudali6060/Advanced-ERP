using Data.Entities.Shared;
using Newtonsoft.Json;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Sales
{
    public class SalesBillHeaderDTO : AuditEntityDTO
    {
        public bool IsActive { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Transfer { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaining { get; set; }
        public string Notes { get; set; }
        public long ClientVendorId { get; set; }
        public string ClientVendorName { get; set; }
        public long? CompanyId { get; set; }

        public virtual List<SalesBillDetailDTO> SalesBillDetailList { get; set; }

    }
}
