

using Data.Entities.Purchases;
using Data.Entities.Sales;
using IdentityModel;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public interface IPurchasesBillHeaderDAL : ICRUDOperationsDAL<PurchasesBillHeader>
    {
        Task<PurchasesBillHeader> GetByNumber(string number);
        Task<IQueryable<PurchasesBillHeader>> GetAllByVendorId(long vendorId);

    }
}
