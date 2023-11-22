using Data.Entities.Accouting;
using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Threading.Tasks;

namespace DataAccess.Accounting.Contracts
{
    public interface ITreasuryDAL : IGenericRepository<Treasury>
    {

    }
}