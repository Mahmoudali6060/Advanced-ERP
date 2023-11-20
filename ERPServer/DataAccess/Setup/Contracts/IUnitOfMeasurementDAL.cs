using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IUnitOfMeasurementDAL : IGenericRepository<UnitOfMeasurement>
    {

    }
}