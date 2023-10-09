

using Data.Entities.Setup;
using Shared.DataAccessLayer;

namespace Setup.DataAccessLayer
{
    public interface ICountryDAL : ICRUDOperationsDAL<Country>
    {
    }
}
