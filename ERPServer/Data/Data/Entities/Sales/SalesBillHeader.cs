using Data.Entities.Setup;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Sales
{
    public class SalesBillHeader : BaseEntity
    {
        public bool IsActive { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Transfer { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Notes { get; set; }
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<SalesBillDetail> SalesBillDetailList { get; set; }

    }
}
