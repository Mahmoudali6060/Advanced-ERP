using Data.Entities.Shared;

namespace Data.Entities.Setup
{
    public class Category : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}