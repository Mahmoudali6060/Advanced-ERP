using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;

namespace Data.Entities.Setup
{
    public class UnitOfMeasurement : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
