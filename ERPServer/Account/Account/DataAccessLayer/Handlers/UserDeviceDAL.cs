using Data.Contexts;
using Data.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public class UserDeviceDAL : IUserDeviceDAL
    {
        private readonly AppDbContext _appDbContext;
        public UserDeviceDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IQueryable<UserDevice>> GetAll()
        {
            return _appDbContext.UserDevices.AsQueryable();
        }

        public async Task<IQueryable<UserDevice>> GetAllLite()
        {
            return _appDbContext.UserDevices.AsQueryable();
        }

        public async Task<UserDevice> GetById(long id)
        {
            var UserDevice = _appDbContext.UserDevices.SingleOrDefaultAsync(x => x.Id == id);
            return await UserDevice;
        }



        public async Task<long> Add(UserDevice entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(UserDevice entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(UserDevice userDevice)
        {
            _appDbContext.UserDevices.Remove(userDevice);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserDevice> GetUserDeviceByUserProfileId(long userProfile)
        {
            return await _appDbContext.UserDevices.SingleOrDefaultAsync(x => x.UserProfileId == userProfile);
        }


    }
}
