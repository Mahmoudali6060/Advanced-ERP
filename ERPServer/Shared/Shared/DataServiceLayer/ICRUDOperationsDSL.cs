using Shared.Entities;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataServiceLayer
{
    public interface ICRUDOperationsDSL<TEntity, TSearch>
    {
        Task<ResponseEntityList<TEntity>> GetAll(TSearch searchCrieria);
        Task<ResponseEntityList<TEntity>> GetAllLite();
        Task<TEntity> GetById(long id);
        Task<long> Add(TEntity entity);
        Task<long> Update(TEntity entity);
        Task<bool> Delete(long id);
    }
}
