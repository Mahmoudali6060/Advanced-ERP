using Data.Entities.Shared;
using System.Collections.Generic;

namespace Data.Entities.Setup
{
    public class Category : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}