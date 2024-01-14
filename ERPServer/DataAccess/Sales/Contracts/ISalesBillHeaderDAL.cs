

using Data.Entities.Purchases;
using Data.Entities.Sales;
using IdentityModel;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public interface ISalesBillHeaderDAL : ICRUDOperationsAsyncDAL<SalesBillHeader>
    {
        Task<SalesBillHeader> GetByNumber(string number);
        Task<IQueryable<SalesBillHeader>> GetAllByClientId(long clientId);
    }
}
