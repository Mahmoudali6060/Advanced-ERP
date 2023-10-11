using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class ProductSearchDTO : Paging
    {
        public bool? IsActive { get; set; }
        public string Code { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public decimal AverageCost { get; set; }
        public decimal LastPurchasedPrice { get; set; }
        public int ActualQuantity { get; set; }
        public int LowQuantity { get; set; }
        public int HighQuantity { get; set; }
        public long CategoryId { get; set; }
    }
}
