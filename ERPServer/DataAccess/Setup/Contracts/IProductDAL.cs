

using Data.Entities.Setup;
using Shared.DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface IProductDAL : ICRUDOperationsDAL<Product>
    {
        Task<IQueryable<Product>> GetAllLiteByCategoryId(long categoryId);
    }
}
