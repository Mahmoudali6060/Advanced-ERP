

using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Shared.DataAccessLayer;

using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public interface IUserProfileDAL : ICRUDOperationsDAL<UserProfile>
    {
        Task<UserProfile> GetUserProfileByAppUserId(string appUserId);
        Task<UserProfile> GetUserProfileByCompanyId(long CompanyId);
        long UpdateSync(UserProfile entity);
        UserProfile GetByIdSync(long id);

    }
}
