using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Entities
{
    public class CarRequestTotalDetails
    {
        public int Total { get; set; }
        public int TotalApproved { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalClosed { get; set; }

    }
}
