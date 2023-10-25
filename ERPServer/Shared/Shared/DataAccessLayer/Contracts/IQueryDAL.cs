using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccessLayer.Contracts
{
    public interface IQueryDAL<TEntity>
    {
        Task<TEntity> GetByNumber(string number);
    }
}
