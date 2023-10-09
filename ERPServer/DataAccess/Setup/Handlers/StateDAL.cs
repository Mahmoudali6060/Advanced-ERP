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
        public async Task<IQueryable<State>> GetAll()
        {
            return _appDbContext.States.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<State>> GetAllLite()
        {
            return _appDbContext.States.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<State>> GetAllLiteByCountryId(long countryId)
        {
            return _appDbContext.States.Where(x => x.CountryId == countryId).AsQueryable();
        }

        public async Task<State> GetById(long id)
        {
            var State = _appDbContext.States.SingleOrDefaultAsync(x => x.Id == id);
            return await State;
        }

        #endregion

        #region Command

        public async Task<long> Add(State entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(State entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(State state)
        {
            _appDbContext.States.Remove(state);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}
