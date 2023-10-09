using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FleetManagement.Entities
{
    public class CarRequestSearchCriteriaDTO : DataSource
    {

        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string PickUp { get; set; }
        public string Destination { get; set; }
        public string Comments { get; set; }
        public string Purpose { get; set; }
        public CarRequestStatusEnum CarRequestStatusId { get; set; }
        public long? UserProfileId { get; set; }
        public string SequenceNumber { get; set; }
    }
}
