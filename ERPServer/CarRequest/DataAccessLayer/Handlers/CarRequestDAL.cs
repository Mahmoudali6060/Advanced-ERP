using Data.Contexts;
using Data.Entities.FleetManagement;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.DataAccessLayer
{
    public class CarRequestDAL : ICarRequestDAL
    {
        private readonly AppDbContext _appDbContext;
        public CarRequestDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IQueryable<CarRequest>> GetAll()
        {
            return _appDbContext.CarRequests.AsQueryable();
        }

        public async Task<IQueryable<CarRequest>> GetAllLite()
        {
            return _appDbContext.CarRequests.OrderByDescending(x=>x.DateFrom).AsQueryable();
        }

        public async Task<CarRequest> GetById(long id)
        {
            return await _appDbContext.CarRequests.SingleOrDefaultAsync(x => x.Id == id);
        }


        public async Task<long> Add(CarRequest entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<long> Update(CarRequest entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(CarRequest userProfile)
        {
            _appDbContext.CarRequests.Remove(userProfile);
            await _appDbContext.SaveChangesAsync();
            return true;
        }



    }
}
