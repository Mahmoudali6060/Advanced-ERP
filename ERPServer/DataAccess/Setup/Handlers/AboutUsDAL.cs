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
    public async Task<long> Add(AboutUs entity)
    {
        _appDbContext.Entry(entity).State = EntityState.Added;
        await _appDbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(AboutUs entity)
    {
        _appDbContext.AboutUs.Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IQueryable<AboutUs>> GetAll()
    {
        return _appDbContext.AboutUs.AsQueryable();
    }

    public async Task<IQueryable<AboutUs>> GetAllLite()
    {
        return _appDbContext.AboutUs.AsQueryable();
    }

    public async Task<AboutUs> GetById(long id)
    {
        var AboutUs = _appDbContext.AboutUs.SingleOrDefaultAsync(x => x.Id == id);
        return await AboutUs;
    }

    public async Task<long> Update(AboutUs entity)
    {
        _appDbContext.Entry(entity).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return entity.Id;
    }
}
}

