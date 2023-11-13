using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class CountryDAL : ICountryDAL
    {
        private readonly AppDbContext _appDbContext;
        public CountryDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<Country>> GetAll()
        {
            return _appDbContext.Countries.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<Country>> GetAllLite()
        {
            return _appDbContext.Countries.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<Country> GetById(long id)
        {
            var Country = _appDbContext.Countries.SingleOrDefaultAsync(x => x.Id == id);
            return await Country;
        }

        #endregion

        #region Command

        public async Task<long> Add(Country entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> Update(Country entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> Delete(Country entity)
        {
            _appDbContext.Countries.Remove(entity);
            return true;
        }

        #endregion

    }
}
