using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Shared.Entities.Shared;
using Shared.Enums;
using System.Collections.Generic;

namespace Data.Entities.Setup
{
    public class Setting : IEntity
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string SalesBillInstructions { get; set; }
        public string PurchasesBillInstructions { get; set; }
        public bool ChangeProductPriceFromSales { get; set; }
        public bool ChangeProductPriceFromPurchases { get; set; }
        public virtual Company Company { get; set; }

    }
}
