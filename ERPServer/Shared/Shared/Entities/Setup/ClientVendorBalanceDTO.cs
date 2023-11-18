using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class ClientVendorBalanceDTO : BaseDTO
    {
        public long ClientVendorId { get; set; }
        public long RefId { get; set; }//Sales Or Purchases
        public string Date { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Details { get; set; }
        public string Number { get; set; }


    }
}
