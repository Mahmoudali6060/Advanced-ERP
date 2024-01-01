using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccessLayer.Contracts
{

    public interface IGenericRepository<TEntity> :ICRUDOperationsDAL<TEntity>
           where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllWithIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}
