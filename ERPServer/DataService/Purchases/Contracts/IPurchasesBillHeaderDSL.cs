using Data.Entities.Purchases;
using Entities.Account;
using IdentityModel;
using Purchases.DataAccessLayer;
using Shared.DataServiceLayer;
using Shared.Entities.Purchases;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Purchases.Contracts
{
    public interface IPurchasesBillHeaderDSL : ICRUDOperationsDSL<PurchasesBillHeaderDTO, PurchasesBillHeaderSearchDTO>
    {
        Task<PurchasesBillHeaderDTO> GetByNumber(string number);

    }
}
