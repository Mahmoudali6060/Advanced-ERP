using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class ClientDAL : IClientDAL
    {
        private readonly AppDbContext _appDbContext;
        public ClientDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<Client>> GetAll()
        {
            return _appDbContext.Clients.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<IQueryable<Client>> GetAllLite()
        {
            return _appDbContext.Clients.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<Client> GetById(long id)
        {
            var Client = _appDbContext.Clients.SingleOrDefaultAsync(x => x.Id == id);
            return await Client;
        }

        #endregion

        #region Command

        public async Task<long> Add(Client entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(Client entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(Client entity)
        {
            _appDbContext.Clients.Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}
