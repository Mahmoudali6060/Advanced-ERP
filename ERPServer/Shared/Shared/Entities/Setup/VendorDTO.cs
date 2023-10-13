using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class VendorDTO : BaseDTO
    {
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string ImageUrl { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }
        public string IdNumber { get; set; }
        public long? VendorId { get; set; }
        public string ImageBase64 { get; set; }
    }
}
