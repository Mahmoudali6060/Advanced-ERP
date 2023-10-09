using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Account.Entities
{
    public class AvailabilitySearchCriteriaDTO
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }
}
