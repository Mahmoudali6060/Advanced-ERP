using Data.Contexts;
using Data.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public class SalesBillHeaderDAL : ISalesBillHeaderDAL
    {
        private readonly AppDbContext _appDbContext;
        public SalesBillHeaderDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<SalesBillHeader>> GetAll()
        {
            return _appDbContext.SalesBillHeaders.Include(x=>x.Client).OrderByDescending(x => x.Date).AsQueryable();
        }

        public async Task<IQueryable<SalesBillHeader>> GetAllLite()
        {
            return _appDbContext.SalesBillHeaders.OrderByDescending(x => x.Date).AsQueryable();
        }

        public async Task<SalesBillHeader> GetById(long id)
        {
            var SalesBillHeader = _appDbContext.SalesBillHeaders.Include(x=>x.Client).Include(x => x.SalesBillDetailList).SingleOrDefaultAsync(x => x.Id == id);
            return await SalesBillHeader;
        }

        #endregion

        #region Command

        public async Task<long> Add(SalesBillHeader entity)
        {
            entity.Number = GenerateSequenceNumber();
            await _appDbContext.AddAsync(entity);
            return entity.Id;
        }

        public async Task<long> Update(SalesBillHeader entity)
        {
            _appDbContext.SalesBillHeaders.Update(entity);
            return entity.Id;
        }

        public async Task<bool> Delete(SalesBillHeader entity)
        {
            _appDbContext.SalesBillHeaders.Remove(entity);
            return true;
        }

        #endregion

        #region Helper
        private string GenerateSequenceNumber()
        {
            var lastElement = _appDbContext.Clients.OrderByDescending(p => p.Id)
                       .FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Code) + 1;
            return code.ToString();
        }
        #endregion

    }
}
