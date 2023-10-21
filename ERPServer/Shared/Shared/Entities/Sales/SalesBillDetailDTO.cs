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
    public class SalesBillDetailDTO : BaseDTO
    {
        public long SalesBillHeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public string Notes { get; set; }

        [JsonIgnore]
        public virtual SalesBillHeaderDTO SalesBillHeader { get; set; }
    }
}
