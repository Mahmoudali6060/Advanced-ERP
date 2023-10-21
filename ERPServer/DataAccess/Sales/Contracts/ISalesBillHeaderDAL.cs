

using Data.Entities.Sales;
using IdentityModel;
using Shared.DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public interface ISalesBillHeaderDAL : ICRUDOperationsDAL<SalesBillHeader>
    {
    }
}
