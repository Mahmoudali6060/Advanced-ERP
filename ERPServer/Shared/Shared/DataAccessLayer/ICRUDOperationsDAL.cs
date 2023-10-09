using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccessLayer
{
    public interface ICRUDOperationsDAL<TEntity>
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetAllLite();
        Task<TEntity> GetById(long id);
        Task<long> Add(TEntity entity);
        Task<long> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);

    }
}
