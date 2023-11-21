﻿using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;
using System;


namespace Shared.Entities.Setup
{
    public class ProductTrackingDTO : BaseDTO
    {
        public long ProductId { get; set; }
        public string Date { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string CreatedByUsername { get; set; }
        public ProductProcessTypeEnum ProductProcessTypeId { get; set; }
    }
}
