using Data.Contexts;
using Data.Entities.Purchases;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Purchases.DataAccessLayer
{
    public class PurchasesBillHeaderDAL : IPurchasesBillHeaderDAL
    {
        private readonly AppDbContext _appDbContext;
        public PurchasesBillHeaderDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<PurchasesBillHeader>> GetAll()
        {
            return _appDbContext.PurchasesBillHeaders.Include(x=>x.ClientVendor).OrderByDescending(x => x.Date).AsQueryable();
        }

        public async Task<IQueryable<PurchasesBillHeader>> GetAllLite()
        {
            return _appDbContext.PurchasesBillHeaders.OrderByDescending(x => x.Date).AsQueryable();
        }

        public async Task<PurchasesBillHeader> GetById(long id)
        {
            var PurchasesBillHeader = _appDbContext.PurchasesBillHeaders.Include(x => x.ClientVendor).Include(x => x.PurchasesBillDetailList).SingleOrDefaultAsync(x => x.Id == id);
            return await PurchasesBillHeader;
        }

        #endregion

        #region Command

        public async Task<long> Add(PurchasesBillHeader entity)
        {
            entity.Number = GenerateSequenceNumber();
            await _appDbContext.AddAsync(entity);
            return entity.Id;
        }

        public async Task<long> Update(PurchasesBillHeader entity)
        {
            _appDbContext.PurchasesBillHeaders.Update(entity);
            return entity.Id;
        }

        public async Task<bool> Delete(PurchasesBillHeader entity)
        {
            _appDbContext.PurchasesBillHeaders.Remove(entity);
            return true;
        }

        #endregion

        #region Helper
        private string GenerateSequenceNumber()
        {
            var lastElement = _appDbContext.ClientVendors.OrderByDescending(p => p.Id)
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
