using Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class Advertisment : BaseEntity
    {
        public string Media { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
