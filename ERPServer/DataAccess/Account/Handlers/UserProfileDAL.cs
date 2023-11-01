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
            return _appDbContext.UserProfiles.Where(x=>x.IsHide == false).Include(x => x.AppUser).Include(x=>x.Role).AsQueryable();
        }

        public async Task<IQueryable<UserProfile>> GetAllLite()
        {
            return _appDbContext.UserProfiles.Include(x => x.AppUser).Include(x=>x.Role).AsQueryable();
        }

        public async Task<UserProfile> GetById(long id)
        {
            var UserProfile = _appDbContext.UserProfiles.Include(c => c.Company).Include(x => x.AppUser).SingleOrDefaultAsync(x => x.Id == id);
            return await UserProfile;
        }

        public UserProfile GetByIdSync(long id)
        {
            var UserProfile = _appDbContext.UserProfiles.Include(c => c.Company).Include(x => x.AppUser).SingleOrDefault(x => x.Id == id);
            return UserProfile;
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

        public long UpdateSync(UserProfile entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.SaveChanges();
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
            return await _appDbContext.UserProfiles.Include(x=>x.Role).ThenInclude(x=>x.RolePrivileges).SingleOrDefaultAsync(x => x.AppUserId == appUserId);
        }
        public async Task<UserProfile> GetUserProfileByCompanyId(long CompanyId)
        {
            return await _appDbContext.UserProfiles
                        .Include(u => u.AppUser).SingleOrDefaultAsync(x => x.CompanyId == CompanyId);
        }

    }
}
