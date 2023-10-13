using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class VendorSearchDTO : Paging
    {
        public bool? IsActive { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }
        public string IdNumber { get; set; }
    }
}
