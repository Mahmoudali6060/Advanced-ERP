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
        public async Task<IQueryable<PurchasesBillDetail>> GetAll()
        {
            return _appDbContext.PurchasesBillDetails.OrderByDescending(x => x.Id).AsQueryable();
        }

        public async Task<IQueryable<PurchasesBillDetail>> GetAllLite()
        {
            return _appDbContext.PurchasesBillDetails.OrderByDescending(x => x.Id).AsQueryable();
        }

        public async Task<PurchasesBillDetail> GetById(long id)
        {
            var PurchasesBillDetail = _appDbContext.PurchasesBillDetails.SingleOrDefaultAsync(x => x.Id == id);
            return await PurchasesBillDetail;
        }

        #endregion

        #region Command

        public async Task<long> Add(PurchasesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<bool> AddRange(List<PurchasesBillDetail> list)
        {
             _appDbContext.PurchasesBillDetails.AddRange(list);
            return true;
        }

        public async Task<long> Update(PurchasesBillDetail entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> Delete(PurchasesBillDetail entity)
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
