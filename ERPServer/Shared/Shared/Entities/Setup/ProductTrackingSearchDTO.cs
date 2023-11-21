using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;
using System;


namespace Shared.Entities.Setup
{
    public class ProductTrackingSearchDTO : Paging
    {
        public long ProductId { get; set; }
        public string Date { get; set; }
        public ProductProcessTypeEnum? ProductProcessTypeId { get; set; }
    }
}
