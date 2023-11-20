using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;

namespace Shared.Entities.Setup
{
    public class RepresentiveSearchDTO : Paging
    {
        public string FullName { get; set; }
        public string AddressDetails { get; set; }
        public string Mobile { get; set; }
        public string Notes { get; set; }
        public RepresentiveTypeEnum? RepresentiveTypeId { get; set; }

    }
}
