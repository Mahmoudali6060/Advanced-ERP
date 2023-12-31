﻿using Data.Entities.Shared;
using Newtonsoft.Json;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shared.Entities.Purchases
{
    public class PurchasesBillDetailDTO : BaseDTO
    {
        public long PurchasesBillHeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public string Notes { get; set; }
        public long? CompanyId { get; set; }
        public bool IsReturned { get; set; }

        [JsonIgnore]
        public virtual PurchasesBillHeaderDTO PurchasesBillHeader { get; set; }
    }
}
