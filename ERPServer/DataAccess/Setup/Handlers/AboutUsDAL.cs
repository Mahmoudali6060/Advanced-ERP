using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Handlers
{
    public class AboutUsDAL : IAboutUsDAL
    { 
    private readonly AppDbContext _appDbContext;

    public AboutUsDAL(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<long> AddAsync(AboutUs entity)
    {
        _appDbContext.Entry(entity).State = EntityState.Added;
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(AboutUs entity)
    {
        _appDbContext.AboutUs.Remove(entity);
        return true;
    }

    public async Task<IQueryable<AboutUs>> GetAllAsync()
    {
        return _appDbContext.AboutUs.AsQueryable();
    }

    public async Task<IQueryable<AboutUs>> GetAllLiteAsync()
    {
        return _appDbContext.AboutUs.AsQueryable();
    }

    public async Task<AboutUs> GetByIdAsync(long id)
    {
        var AboutUs = _appDbContext.AboutUs.SingleOrDefaultAsync(x => x.Id == id);
        return await AboutUs;
    }

    public async Task<long> UpdateAsync(AboutUs entity)
    {
        _appDbContext.Entry(entity).State = EntityState.Modified;
        return entity.Id;
    }
}
}

