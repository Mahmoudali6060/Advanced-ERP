using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Setup
{
    public class CategorySearchDTO : Paging
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
    }
}
