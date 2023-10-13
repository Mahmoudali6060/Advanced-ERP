using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class Vendor : BaseEntity
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
        public long? ClientId { get; set; }
    }
}
