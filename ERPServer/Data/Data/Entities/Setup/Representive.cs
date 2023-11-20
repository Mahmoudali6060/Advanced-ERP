﻿using Data.Entities.Shared;
using Shared.Enums;

namespace Data.Entities.Setup
{
    public class Representive : BaseEntity
    {
        public string FullName { get; set; }
        public string AddressDetails { get; set; }
        public string Mobile { get; set; }
        public string Notes { get; set; }
        public RepresentiveTypeEnum RepresentiveTypeId { get; set; }

    }
}
