using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Entities
{
    public class TripSearchCriteriaDTO : DataSource
    {
        public TripStatusEnum TripStatusId { get; set; }
        public int PreviousKiloMeterCounter { get; set; }
        public int CurrentKiloMeterCounter { get; set; }
        public string ActualStartTime { get; set; }
        public string ActualEndTime { get; set; }
        public TripClosingReasonEnum TripClosingReasonId { get; set; }
        public string SequenceNumber { get; set; }
        public long DriverId { get; set; }
    }
}
