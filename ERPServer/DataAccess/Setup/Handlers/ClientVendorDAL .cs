using Data.Contexts;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class ClientVendorDAL : IClientVendorDAL
    {
        private readonly AppDbContext _appDbContext;
        public ClientVendorDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<ClientVendor>> GetAll()
        {
            return _appDbContext.ClientVendors.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<IQueryable<ClientVendor>> GetAllLite()
        {
            return _appDbContext.ClientVendors.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<ClientVendor> GetById(long id)
        {
            var ClientVendor = _appDbContext.ClientVendors.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return await ClientVendor;
        }

        #endregion

        #region Command

        public async Task<long> Add(ClientVendor entity)
        {
            var exsited = _appDbContext.ClientVendors.SingleOrDefault(x => x.FullName == entity.FullName);
            if (exsited != null)
            {
                throw new Exception("Errors.DuplicatedFullName");
            }
            entity.Code = GenerateSequenceNumber();
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> Update(ClientVendor entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> Delete(ClientVendor entity)
        {
            _appDbContext.ClientVendors.Remove(entity);
            return true;
        }

        #endregion

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

    }
}
