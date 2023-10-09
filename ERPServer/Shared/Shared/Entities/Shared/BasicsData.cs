using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Shared
{
    public class BasicsData : BaseEntity
    {
        public DateTime PayDate { get; set; }
        public decimal Pay { get; set; }
        public DateTime? WithdrawsDate { get; set; }
        public decimal? Withdraws { get; set; }
        public decimal? Balance { get; set; }
    }
}
