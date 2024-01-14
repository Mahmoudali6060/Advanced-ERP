

using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public interface IClientVendorDAL : ICRUDOperationsAsyncDAL<ClientVendor>
    {

    }
}
