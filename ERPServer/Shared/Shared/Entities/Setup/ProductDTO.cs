using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class ProductDTO : AuditEntityDTO
    {
        public bool IsActive { get; set; }
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
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageBase64 { get; set; }
        public long? CompanyId { get; set; }
        public long? UnitOfMeasurementId { get; set; }
        public long? UnitOfMeasurementName { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }

    }
}
