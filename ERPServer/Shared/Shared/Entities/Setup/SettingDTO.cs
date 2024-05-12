using Data.Entities.Shared;
using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;

namespace Shared.Entities.Setup
{
    public class SettingDTO
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string SalesBillInstructions { get; set; }
        public string PurchasesBillInstructions { get; set; }
        public bool ChangeProductPriceFromSales { get; set; }
        public bool ChangeProductPriceFromPurchases { get; set; }
    }
}
