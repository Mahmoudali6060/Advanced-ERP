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
    public class SalesBillDetail : BaseEntity
    {
        public long SalesBillHeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public string Notes { get; set; }
        public virtual Product Product { get; set; }
        public virtual SalesBillHeader SalesBillHeader { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
