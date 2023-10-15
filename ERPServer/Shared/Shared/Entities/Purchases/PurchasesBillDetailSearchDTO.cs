using Data.Entities.Shared;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Purchases
{
    public class PurchasesBillDetailSearchDTO : Paging
    {
        public long PurchasesBillHeaderId { get; set; }
        public long ProductId { get; set; }
    }
}
