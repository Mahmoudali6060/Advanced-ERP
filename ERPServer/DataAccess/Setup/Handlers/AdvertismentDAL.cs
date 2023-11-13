using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Handlers
{
    public class AdvertismentDAL : IAdvertismentDAL
    {
        private readonly AppDbContext _appDbContext;

        public AdvertismentDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<long> Add(Advertisment entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> AddRang(List<Advertisment> lstAdvertisments)
        {
           _appDbContext.AddRange(lstAdvertisments);
            return lstAdvertisments.Count;
        }

        public async Task<bool> Delete(Advertisment entity)
        {
            _appDbContext.Advertisments.Remove(entity);
            return true;
        }

        public async Task<IQueryable<Advertisment>> GetAll()
        {
            return _appDbContext.Advertisments.AsQueryable();
        }

        public async Task<IQueryable<Advertisment>> GetAllLite()
        {
            return _appDbContext.Advertisments.AsQueryable();
        }

        public async Task<Advertisment> GetById(long id)
        {
            var Advertisment = _appDbContext.Advertisments.SingleOrDefaultAsync(x => x.Id == id);
            return await Advertisment;
        }

        public async Task<long> Update(Advertisment entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }
    }
}

