﻿using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class CityDAL : ICityDAL
    {
        private readonly AppDbContext _appDbContext;
        public CityDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<City>> GetAllAsync()
        {
            return _appDbContext.Cities.OrderBy(x=>x.Name).AsQueryable();
        }

        public async Task<IQueryable<City>> GetAllLiteAsync()
        {
            return _appDbContext.Cities.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<City>> GetAllLiteByStateId(long stateId)
        {
            return _appDbContext.Cities.Where(x => x.StateId == stateId).AsQueryable();
        }

        public async Task<City> GetByIdAsync(long id)
        {
            var City = _appDbContext.Cities.SingleOrDefaultAsync(x => x.Id == id);
            return await City;
        }

        #endregion

        #region Command

        public async Task<long> AddAsync(City entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> UpdateAsync(City entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(City entity)
        {
            _appDbContext.Cities.Remove(entity);
            return true;
        }

        #endregion

    }
}
