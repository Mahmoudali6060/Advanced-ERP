using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Entities
{
    public class CarRequestDetailDTO
    {
        public int TotalApproved { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalClosed { get; set; }
    }
}
