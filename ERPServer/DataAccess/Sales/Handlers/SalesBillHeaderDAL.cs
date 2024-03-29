﻿using Data.Contexts;
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
        public async Task<IQueryable<SalesBillHeader>> GetAllAsync()
        {
            return _appDbContext.SalesBillHeaders.Include(x => x.ClientVendor).Include(x => x.CreatedByProfile).Include(x => x.ModifiedByProfile).OrderByDescending(x => x.Id).AsQueryable();
        }

        public async Task<IQueryable<SalesBillHeader>> GetAllLiteAsync()
        {
            return _appDbContext.SalesBillHeaders.OrderByDescending(x => x.Date).AsQueryable();
        }

        public async Task<SalesBillHeader> GetByIdAsync(long id)
        {
            var SalesBillHeader = _appDbContext.SalesBillHeaders.Include(x => x.ClientVendor).Include(x => x.SalesBillDetailList).Include(x => x.AccountStatement).SingleOrDefaultAsync(x => x.Id == id);
            return await SalesBillHeader;
        }

        public async Task<IQueryable<SalesBillHeader>> GetAllByClientId(long clientId)
        {
            return _appDbContext.SalesBillHeaders.Include(x => x.ClientVendor).Where(x => x.ClientVendorId == clientId).AsQueryable();
        }

        public async Task<SalesBillHeader> GetByNumber(string number)
        {
            var SalesBillHeader = _appDbContext.SalesBillHeaders.Include(x => x.ClientVendor).Include(x => x.SalesBillDetailList).SingleOrDefaultAsync(x => x.Number == number);
            return await SalesBillHeader;
        }

        #endregion

        #region Command

        public async Task<long> AddAsync(SalesBillHeader entity)
        {
            //entity.Number = GenerateSequenceNumber();
            await _appDbContext.AddAsync(entity);
            return entity.Id;
        }

        public async Task<long> UpdateAsync(SalesBillHeader entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Entry(entity).Property(x => x.Number).IsModified = false;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(SalesBillHeader entity)
        {
            _appDbContext.SalesBillHeaders.Remove(entity);
            return true;
        }

        #endregion

        #region Helper
        private string GenerateSequenceNumber()
        {
            lock (this)
            {
                var lastElement = _appDbContext.SalesBillHeaders.OrderByDescending(p => p.Id)
                      .FirstOrDefault();
                if (lastElement == null)
                {
                    return "1000";
                }
                int code = int.Parse(lastElement.Number) + 1;
                return code.ToString();
            }
           
        }
        #endregion

    }
}
