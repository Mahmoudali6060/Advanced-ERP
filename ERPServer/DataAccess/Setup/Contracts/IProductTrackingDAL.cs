using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IProductTrackingDAL : IGenericRepository<ProductTracking>
    {
        Task<IQueryable<ProductTracking>> GetProductTrackingByProductId(long productId);

    }
}