﻿using Shared.Entities.Shared;
using System;

namespace Data.Entities.Sales
{
    public class SalesBillHeaderSearchDTO : Paging
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
        public bool? IsTemp { get; set; }
        public bool? IsReturned { get; set; }
        public long? ClientVendorId { get; set; }
        public string PersonPhoneNumber { get; set; }
    }
}
