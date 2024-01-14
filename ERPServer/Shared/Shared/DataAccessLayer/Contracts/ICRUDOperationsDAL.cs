using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccessLayer.Contracts
{
    public interface ICRUDOperationsDAL<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllLite();
        TEntity GetById(long id);
        long Add(TEntity entity);
        long Update(TEntity entity);
        bool Delete(TEntity entity);

    }
}
