using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Enums
{
    public enum ProductProcessTypeEnum
    {
        Created = 1,
        ChangePrice = 2,
        ChangeQuantity = 3,
        AddedToSalesBill = 4,
        AddedToPurchases = 5,
        Deleted = 6
    }
}
