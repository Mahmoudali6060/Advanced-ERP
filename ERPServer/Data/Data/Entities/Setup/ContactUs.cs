using Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class ContactUs : BaseEntity
    {
        public string Name { get; set; }
        public string Email  { get; set; }
        public string Mobile { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
