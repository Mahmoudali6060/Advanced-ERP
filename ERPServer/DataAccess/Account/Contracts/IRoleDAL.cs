

using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Shared.DataAccessLayer.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public interface IRoleDAL : ICRUDOperationsAsyncDAL<RoleGroup>
    {

    }
}
