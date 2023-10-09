using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FleetManagement.Entities
{
    public class CarRequestDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Errors.RequesterNameIsRequired")]
        public string RequesterName { get; set; }

        [Required(ErrorMessage = "Errors.RequesterEmailIsRequired")]
        [EmailAddress(ErrorMessage = "Errors.InvlidEmailAddress")]
        public string RequesterEmail { get; set; }

        [Required(ErrorMessage = "Errors.DateFromIsRequired")]
        public string DateFrom { get; set; }

        [Required(ErrorMessage = "Errors.DateToIsRequired")]
        public string DateTo { get; set; }
        public string PickUp { get; set; }
        public string Destination { get; set; }
        public string Comments { get; set; }
        public string Purpose { get; set; }
        public string SequenceNumber { get; set; }
        public CarRequestStatusEnum CarRequestStatusId { get; set; }
        public long? SupervisorId { get; set; }
        public long? DriverId { get; set; }
        public ClosingReasonEnum ClosingReasonId { get; set; }
        public string ClosingReasonComment { get; set; }

    }
}
