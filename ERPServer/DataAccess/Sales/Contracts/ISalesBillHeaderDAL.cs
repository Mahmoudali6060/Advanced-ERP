

using Data.Entities.Purchases;
using Data.Entities.Sales;
using IdentityModel;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public interface ISalesBillHeaderDAL : ICRUDOperationsDAL<SalesBillHeader>
    {
        Task<SalesBillHeader> GetByNumber(string number);
    }
}
