using Data.Contexts;
using Data.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Shared.Handlers
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

       

        public async Task<IQueryable<TEntity>> GetAllWithIncludes(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = null;
            if (predicate == null)
            {
                query = GetAllAsync().Result;

            }
            else
            {
                query = GetAllAsync().Result.Where(predicate);

            }
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        //public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    TEntity entity = _dbContext.Set<TEntity>()
        //        .AsNoTracking()
        //        .FirstOrDefault(predicate);
        //    return includes.Aggregate(entity, (current, includeProperty) => current.Include(includeProperty));
        //}

        public async Task<IQueryable<TEntity>> GetAllLiteAsync()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }


        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<long> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<long> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var exsitedEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            _dbContext.Set<TEntity>().Remove(exsitedEntity);
            return true;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAllLite()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetById(long id)
        {
            return _dbContext.Set<TEntity>()
                 .AsNoTracking()
                 .FirstOrDefault(e => e.Id == id);
        }

        public long Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public long Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity.Id;
        }

        public bool Delete(TEntity entity)
        {
            var exsitedEntity = _dbContext.Set<TEntity>().Find(entity.Id);
            _dbContext.Set<TEntity>().Remove(exsitedEntity);
            return true;
        }

    
    }

}
