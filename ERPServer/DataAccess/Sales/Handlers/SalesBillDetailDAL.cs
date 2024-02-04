using Data.Contexts;
using Data.Entities.Sales;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.DataAccessLayer
{
    public class SalesBillDetailDAL : ISalesBillDetailDAL
    {
        private readonly AppDbContext _appDbContext;
        public SalesBillDetailDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<SalesBillDetail>> GetAllAsync()
        {
            return _appDbContext.SalesBillDetails.OrderBy(x => x.Id).AsQueryable();
        }

        public async Task<IQueryable<SalesBillDetail>> GetAllLiteAsync()
        {
            return _appDbContext.SalesBillDetails.OrderBy(x => x.Id).AsQueryable();
        }

        public async Task<SalesBillDetail> GetByIdAsync(long id)
        {
            var SalesBillDetail = _appDbContext.SalesBillDetails.SingleOrDefaultAsync(x => x.Id == id);
            return await SalesBillDetail;
        }

        #endregion

        #region Command

        public async Task<long> AddAsync(SalesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<bool> AddRange(List<SalesBillDetail> list)
        {
             _appDbContext.SalesBillDetails.AddRange(list);
            return true;
        }

        public async Task<long> UpdateAsync(SalesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(SalesBillDetail entity)
        {
            _appDbContext.SalesBillDetails.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteRange(List<SalesBillDetail> list)
        {
            _appDbContext.ChangeTracker.Clear();
            _appDbContext.SalesBillDetails.RemoveRange(list);
            return true;
        }

        public async Task<IQueryable<SalesBillDetail>> GetAllByHeaderId(long headerId)
        {
            return _appDbContext.SalesBillDetails.Where(x => x.SalesBillHeaderId==headerId).AsQueryable();
           
        }

        #endregion

    }
}
