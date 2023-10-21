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
        public async Task<IQueryable<SalesBillDetail>> GetAll()
        {
            return _appDbContext.SalesBillDetails.OrderByDescending(x => x.Id).AsQueryable();
        }

        public async Task<IQueryable<SalesBillDetail>> GetAllLite()
        {
            return _appDbContext.SalesBillDetails.OrderByDescending(x => x.Id).AsQueryable();
        }

        public async Task<SalesBillDetail> GetById(long id)
        {
            var SalesBillDetail = _appDbContext.SalesBillDetails.SingleOrDefaultAsync(x => x.Id == id);
            return await SalesBillDetail;
        }

        #endregion

        #region Command

        public async Task<long> Add(SalesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> AddRange(List<SalesBillDetail> list)
        {
             _appDbContext.SalesBillDetails.AddRange(list);
            return true;
        }

        public async Task<long> Update(SalesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(SalesBillDetail entity)
        {
            _appDbContext.SalesBillDetails.Remove(entity);
            await _appDbContext.SaveChangesAsync();
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
