

using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;

namespace Setup.DataAccessLayer
{
    public interface ICountryDAL : ICRUDOperationsAsyncDAL<Country>
    {
    }
}
