using Data.Entities.Shared;
using System.Collections.Generic;

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
        public decimal ActualQuantity { get; set; }
        public decimal LowQuantity { get; set; }
        public decimal HighQuantity { get; set; }
        public string Description { get; set; }
        public long? UnitOfMeasurementId { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<ProductTracking> ProductTrackings { get; set; }

    }
}
