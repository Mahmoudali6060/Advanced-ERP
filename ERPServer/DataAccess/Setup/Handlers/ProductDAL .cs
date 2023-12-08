using Data.Contexts;
using Data.Entities.Setup;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class ProductDAL : IProductDAL
    {
        private readonly AppDbContext _appDbContext;
        public ProductDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<Product>> GetAll()
        {
            return _appDbContext.Products.Include(x => x.Category).OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<Product>> GetAllLite()
        {
            return _appDbContext.Products.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<Product> GetById(long id)
        {
            var Product = _appDbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            return await Product;
        }

        public async Task<IQueryable<Product>> GetAllLiteByCategoryId(long categoryId)
        {
            return _appDbContext.Products.Where(x => x.CategoryId == categoryId).AsQueryable();
        }
        #endregion

        #region Command

        public async Task<long> Add(Product entity)
        {
            entity.Code = GenerateSequenceNumber();
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }



        public async Task<long> Update(Product entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Entry(entity).Property(x => x.Code).IsModified = false;
            return entity.Id;
        }

        public async Task<bool> UpdateAll(List<Product> entityList)
        {
            _appDbContext.Products.UpdateRange(entityList);
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            _appDbContext.Products.Remove(entity);
            return true;
        }

        #endregion

        #region Helper
        private string GenerateSequenceNumber()
        {
            var lastElement = _appDbContext.Products.OrderByDescending(p => p.Id)
                       .FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Code) + 1;
            return code.ToString();
        }
        #endregion

    }
}
