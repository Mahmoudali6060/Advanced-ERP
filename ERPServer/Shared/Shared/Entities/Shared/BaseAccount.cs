using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Shared
{
    public class BaseAccount : BaseEntity
    {
        public long SafeId { get; set; }
        public decimal? PaidUp { get; set; }
        public DateTime? PaidDate { get; set; }
        public string RecieptNumber { get; set; }
    }
}
