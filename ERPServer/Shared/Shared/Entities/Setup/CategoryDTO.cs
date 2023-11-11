using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class CategoryDTO :BaseDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long? CompanyId { get; set; }
    }
}
