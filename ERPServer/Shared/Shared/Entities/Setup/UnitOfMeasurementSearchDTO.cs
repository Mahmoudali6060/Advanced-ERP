using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;

namespace Shared.Entities.Setup
{
    public class UnitOfMeasurementSearchDTO : Paging
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
