using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class ClientVendorSearchDTO : Paging
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool? IsActive { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }
        public string IdNumber { get; set; }
        public ClientVendorTypeEnum TypeId { get; set; }
    }
}
