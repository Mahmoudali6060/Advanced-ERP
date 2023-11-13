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
    public class ContactUsDAL : IContactUsDAL
    {
        private readonly AppDbContext _appDbContext;

        public ContactUsDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<long> Add(ContactUs entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<bool> Delete(ContactUs entity)
        {
            _appDbContext.ContactUs.Remove(entity);
            return true;
        }

        public async Task<IQueryable<ContactUs>> GetAll()
        {
            return _appDbContext.ContactUs.AsQueryable();
        }

        public async Task<IQueryable<ContactUs>> GetAllLite()
        {
            return _appDbContext.ContactUs.AsQueryable();
        }

        public async Task<ContactUs> GetById(long id)
        {
            var contactUs = _appDbContext.ContactUs.SingleOrDefaultAsync(x => x.Id == id);
            return await contactUs;
        }

        public async Task<long> Update(ContactUs entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }
    }
}

