using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class StateDAL : IStateDAL
    {
        private readonly AppDbContext _appDbContext;
        public StateDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<State>> GetAllAsync()
        {
            return _appDbContext.States.OrderByDescending(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<State>> GetAllLiteAsync()
        {
            return _appDbContext.States.OrderByDescending(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<State>> GetAllLiteByCountryId(long countryId)
        {
            return _appDbContext.States.Where(x => x.CountryId == countryId).AsQueryable();
        }

        public async Task<State> GetByIdAsync(long id)
        {
            var State = _appDbContext.States.SingleOrDefaultAsync(x => x.Id == id);
            return await State;
        }

        #endregion

        #region Command

        public async Task<long> AddAsync(State entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> UpdateAsync(State entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(State state)
        {
            _appDbContext.States.Remove(state);
            return true;
        }

        #endregion

    }
}
