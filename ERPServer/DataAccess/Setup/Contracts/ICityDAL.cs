

using Data.Entities.Setup;
using Shared.DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface ICityDAL : ICRUDOperationsDAL<City>
    {
        Task<IQueryable<City>> GetAllLiteByStateId(long stateId);
    }
}
