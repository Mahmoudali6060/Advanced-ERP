

using Account.Entities;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Shared.DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public interface IUserDeviceDAL : ICRUDOperationsDAL<UserDevice>
    {
        Task<UserDevice> GetUserDeviceByUserProfileId(long userProfileId);
    }
}
