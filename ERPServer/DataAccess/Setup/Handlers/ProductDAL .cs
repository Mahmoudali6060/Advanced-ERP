using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Shared.Handlers;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class ProductDAL :  GenericRepository<Product>, IProductDAL
    {
        private readonly AppDbContext _appDbContext;
        public ProductDAL(AppDbContext dbContext)
       : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        #region Query
        public async Task<IQueryable<Product>> GetAllLiteByCategoryId(long categoryId)
        {
            return _appDbContext.Products.Where(x => x.CategoryId == categoryId).AsQueryable();
        }
        #endregion

        #region Command
        public async Task<bool> UpdateAll(List<Product> entityList)
        {
            _appDbContext.Products.UpdateRange(entityList);
            return true;
        }

        #endregion

    
    }
}
