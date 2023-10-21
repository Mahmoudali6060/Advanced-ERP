﻿using Data.Entities.Shared;
using Newtonsoft.Json;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Sales
{
    public class SalesBillHeaderDTO : BaseDTO
    {
        public bool IsActive { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Transfer { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Notes { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }

        public virtual List<SalesBillDetailDTO> SalesBillDetailList { get; set; }

    }
}
