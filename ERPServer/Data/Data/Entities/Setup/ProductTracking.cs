using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;
using System;


namespace Data.Entities.Setup
{
    public class ProductTracking : AuditEntity, IEntity
    {
        public long ProductId { get; set; }
        public DateTime Date { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public ProductProcessTypeEnum ProductProcessTypeId { get; set; }
        public virtual Product Product { get; set; }
    }
}
