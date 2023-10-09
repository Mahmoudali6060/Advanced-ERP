using Data.Contexts;
using Data.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public class UserProfileDAL : IUserProfileDAL
    {
        private readonly AppDbContext _appDbContext;
        public UserProfileDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IQueryable<UserProfile>> GetAll()
        {
            return _appDbContext.UserProfiles.Include(x => x.AppUser).AsQueryable();
        }

        public async Task<IQueryable<UserProfile>> GetAllLite()
        {
            return _appDbContext.UserProfiles.AsQueryable();
        }

        public async Task<UserProfile> GetById(long id)
        {
            var UserProfile = _appDbContext.UserProfiles.Include(x => x.AppUser).SingleOrDefaultAsync(x => x.Id == id);
            return await UserProfile;
        }


        public async Task<IQueryable<UserProfile>> GetAllDriver()
        {
            return  _appDbContext.UserProfiles.Include(x => x.DriverCarRequests).Include(x=>x.UserDevice).Where(x =>
            x.UserTypeId == UserTypeEnum.Driver).AsQueryable();
        }

        public async Task<long> Add(UserProfile entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(UserProfile entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(UserProfile userProfile)
        {
            _appDbContext.UserProfiles.Remove(userProfile);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfile> GetUserProfileByAppUserId(string appUserId)
        {
            return await _appDbContext.UserProfiles.SingleOrDefaultAsync(x => x.AppUserId == appUserId);
        }


    }
}
