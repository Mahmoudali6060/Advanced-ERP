

using Data.Entities.Sales;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public interface ISalesBillDetailDAL : ICRUDOperationsDAL<SalesBillDetail>
    {
        Task<bool> AddRange(List<SalesBillDetail> list);
        Task<bool> DeleteRange(List<SalesBillDetail> list);
        Task<IQueryable<SalesBillDetail>> GetAllByHeaderId(long headerId);
    }
}
