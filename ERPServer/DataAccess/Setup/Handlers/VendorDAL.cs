using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class VendorDAL : IVendorDAL
    {
        private readonly AppDbContext _appDbContext;
        public VendorDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<Vendor>> GetAll()
        {
            return _appDbContext.Vendors.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<IQueryable<Vendor>> GetAllLite()
        {
            return _appDbContext.Vendors.OrderBy(x => x.FullName).AsQueryable();
        }

        public async Task<Vendor> GetById(long id)
        {
            var Vendor = _appDbContext.Vendors.SingleOrDefaultAsync(x => x.Id == id);
            return await Vendor;
        }

        #endregion

        #region Command

        public async Task<long> Add(Vendor entity)
        {
            entity.Code = GenerateSequenceNumber();
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(Vendor entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(Vendor entity)
        {
            _appDbContext.Vendors.Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        #endregion

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

    }
}
