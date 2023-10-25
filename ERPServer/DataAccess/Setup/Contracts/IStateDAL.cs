

using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface IStateDAL : ICRUDOperationsDAL<State>
    {
        Task<IQueryable<State>> GetAllLiteByCountryId(long countryId);
    }
}
