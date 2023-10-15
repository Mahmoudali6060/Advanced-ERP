

using Data.Entities.Purchases;
using Shared.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public interface IPurchasesBillDetailDAL : ICRUDOperationsDAL<PurchasesBillDetail>
    {
        Task<bool> AddRange(List<PurchasesBillDetail> list);
        Task<bool> DeleteRange(List<PurchasesBillDetail> list);
        Task<IQueryable<PurchasesBillDetail>> GetAllByHeaderId(long headerId);
    }
}
