using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal SellingPricePercentage { get; set; }//سعر البيع
        public decimal PurchasingPricePercentage { get; set; }//سعر الشراء
        public int ActualQuantity { get; set; }
        public int LowQuantity { get; set; }
        public int HighQuantity { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
