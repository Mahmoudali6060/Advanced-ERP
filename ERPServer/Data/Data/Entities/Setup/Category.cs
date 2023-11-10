using Data.Entities.Shared;

namespace Data.Entities.Setup
{
    public class Category : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}