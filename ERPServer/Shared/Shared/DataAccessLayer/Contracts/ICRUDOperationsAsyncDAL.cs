using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccessLayer.Contracts
{
    public interface ICRUDOperationsAsyncDAL<TEntity>
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllLiteAsync();
        Task<TEntity> GetByIdAsync(long id);
        Task<long> AddAsync(TEntity entity);
        Task<long> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

    }
}
