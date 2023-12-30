﻿using Data.Contexts;
using Data.Entities.Purchases;
using Data.Entities.Sales;
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
            return _appDbContext.PurchasesBillHeaders.Include(x => x.ClientVendor).Include(x => x.CreatedByProfile).Include(x => x.ModifiedByProfile).OrderByDescending(x => x.Date).ThenBy(x => x.Id).AsQueryable();
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

        public async Task<IQueryable<PurchasesBillHeader>> GetAllByVendorId(long vendorId)
        {
            return _appDbContext.PurchasesBillHeaders.Include(x => x.ClientVendor).Where(x => x.ClientVendorId == vendorId).AsQueryable();
        }

        public async Task<PurchasesBillHeader> GetByNumber(string number)
        {
            var PurchasesBillHeader = _appDbContext.PurchasesBillHeaders.Include(x => x.ClientVendor).Include(x => x.PurchasesBillDetailList).SingleOrDefaultAsync(x => x.Number == number);
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
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Entry(entity).Property(x => x.Number).IsModified = false;
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
            var lastElement = _appDbContext.PurchasesBillHeaders.OrderByDescending(p => p.Id)
                       .FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Number) + 1;
            return code.ToString();
        }
        #endregion

    }
}
