

using Data.Entities.Purchases;
using IdentityModel;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public interface IPurchasesBillHeaderDAL : ICRUDOperationsDAL<PurchasesBillHeader>
    {
        Task<PurchasesBillHeader> GetByNumber(string number);
    }
}
