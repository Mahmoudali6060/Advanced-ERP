using Data.Contexts;
using Data.Entities.Purchases;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public class PurchasesBillDetailDAL : IPurchasesBillDetailDAL
    {
        private readonly AppDbContext _appDbContext;
        public PurchasesBillDetailDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<PurchasesBillDetail>> GetAllAsync()
        {
            return _appDbContext.PurchasesBillDetails.OrderBy(x => x.Id).AsQueryable();
        }

        public async Task<IQueryable<PurchasesBillDetail>> GetAllLiteAsync()
        {
            return _appDbContext.PurchasesBillDetails.OrderBy(x => x.Id).AsQueryable();
        }

        public async Task<PurchasesBillDetail> GetByIdAsync(long id)
        {
            var PurchasesBillDetail = _appDbContext.PurchasesBillDetails.SingleOrDefaultAsync(x => x.Id == id);
            return await PurchasesBillDetail;
        }

        #endregion

        #region Command

        public async Task<long> AddAsync(PurchasesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<bool> AddRange(List<PurchasesBillDetail> list)
        {
             _appDbContext.PurchasesBillDetails.AddRange(list);
            return true;
        }

        public async Task<long> UpdateAsync(PurchasesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(PurchasesBillDetail entity)
        {
            _appDbContext.PurchasesBillDetails.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteRange(List<PurchasesBillDetail> list)
        {
            _appDbContext.ChangeTracker.Clear();
            _appDbContext.PurchasesBillDetails.RemoveRange(list);
            return true;
        }

        public async Task<IQueryable<PurchasesBillDetail>> GetAllByHeaderId(long headerId)
        {
            return _appDbContext.PurchasesBillDetails.Where(x => x.PurchasesBillHeaderId==headerId).AsQueryable();
        }

        #endregion

    }
}
