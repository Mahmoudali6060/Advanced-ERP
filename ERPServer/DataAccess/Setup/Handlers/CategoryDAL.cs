using Data.Contexts;
using Data.Entities.Setup;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly AppDbContext _appDbContext;
        public CategoryDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Query
        public async Task<IQueryable<Category>> GetAll()
        {
            return _appDbContext.Categories.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<IQueryable<Category>> GetAllLite()
        {
            return _appDbContext.Categories.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<Category> GetById(long id)
        {
            var Category = _appDbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
            return await Category;
        }

        #endregion

        #region Command

        public async Task<long> Add(Category entity)
        {
            entity.Code = GenerateSequenceNumber();
            _appDbContext.Entry(entity).State = EntityState.Added;
            return entity.Id;
        }

        public async Task<long> Update(Category entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> Delete(Category entity)
        {
            _appDbContext.Categories.Remove(entity);
            return true;
        }

        #endregion

        #region Helper
        private string GenerateSequenceNumber()
        {
            var lastElement = _appDbContext.Categories.OrderByDescending(p => p.Id)
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
