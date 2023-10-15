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
    public class PurchasesBillDetail : BaseEntity
    {
        public long PurchasesBillHeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public int SubTotal { get; set; }
        public string Notes { get; set; }
        public virtual Product Product { get; set; }
        public virtual PurchasesBillHeader PurchasesBillHeader { get; set; }
    }
}
