using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.Shared
{
    public class SettingsDTO :BaseDTO
    {
        public DateTime ExpiryDate { get; set; }
    }
}
