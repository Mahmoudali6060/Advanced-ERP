using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Entities
{
    public class TripDTO
    {
        public long Id { get; set; }
        public TripStatusEnum TripStatusId { get; set; }
        public int PreviousKiloMeterCounter { get; set; }
        public int CurrentKiloMeterCounter { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ActualEndTime { get; set; }
        public TripClosingReasonEnum TripClosingReasonId { get; set; }
        public long CarRequestId { get; set; }
        public long DriverId { get; set; }
        public string DriverName { get; set; }
        public string SequenceNumber { get; set; }

    }
}
