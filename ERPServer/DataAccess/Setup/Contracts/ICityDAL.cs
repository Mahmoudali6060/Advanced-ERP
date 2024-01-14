

using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface ICityDAL : ICRUDOperationsAsyncDAL<City>
    {
        Task<IQueryable<City>> GetAllLiteByStateId(long stateId);
    }
}
