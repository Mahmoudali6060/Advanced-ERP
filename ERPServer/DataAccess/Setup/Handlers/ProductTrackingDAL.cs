using System.Linq;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
using DataAccess.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Setup.DataAccessLayer
{
    public class ProductTrackingDAL : GenericRepository<ProductTracking>, IProductTrackingDAL
    {
        private readonly AppDbContext _dbContext;
        public ProductTrackingDAL(AppDbContext dbContext)
        : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<ProductTracking>> GetProductTrackingByProductId(long productId)
        {
            return _dbContext.ProductTrackings.Where(x => x.ProductId == productId).OrderByDescending(x => x.Date).AsQueryable();
        }
    }
}