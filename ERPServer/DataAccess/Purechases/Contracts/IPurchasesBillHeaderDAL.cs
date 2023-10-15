

using Data.Entities.Purchases;
using IdentityModel;
using Shared.DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public interface IPurchasesBillHeaderDAL : ICRUDOperationsDAL<PurchasesBillHeader>
    {
    }
}
