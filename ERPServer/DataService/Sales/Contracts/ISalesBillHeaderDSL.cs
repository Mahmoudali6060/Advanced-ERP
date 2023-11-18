using Data.Entities.Sales;
using Entities.Account;
using IdentityModel;
using Sales.DataAccessLayer;
using Shared.DataServiceLayer;
using Shared.Entities.Purchases;
using Shared.Entities.Sales;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Sales.Contracts
{
    public interface ISalesBillHeaderDSL : ICRUDOperationsDSL<SalesBillHeaderDTO, SalesBillHeaderSearchDTO>
    {
        Task<SalesBillHeaderDTO> GetByNumber(string number);
        Task<List<ClientVendorBalanceDTO>> GetAllByClientId(long clientId);

    }
}
