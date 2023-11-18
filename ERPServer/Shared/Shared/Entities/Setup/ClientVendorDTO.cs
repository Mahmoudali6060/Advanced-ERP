using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class ClientVendorDTO:BaseDTO
    {
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string ImageUrl { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal OppeningBalance { get; set; }
        public string Notes { get; set; }
        public string IdNumber { get; set; }
        public string ImageBase64 { get; set; }
        public ClientVendorTypeEnum TypeId { get; set; }
        public long? CompanyId { get; set; }

    }
}
