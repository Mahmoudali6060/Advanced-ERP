

using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface IProductDAL :IGenericRepository<Product>
    {
        Task<IQueryable<Product>> GetAllLiteByCategoryId(long categoryId);
        Task<bool> UpdateAll(List<Product> entityList);
    }
}
