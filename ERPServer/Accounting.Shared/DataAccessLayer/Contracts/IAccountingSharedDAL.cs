
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accouting.Shared.DataAccessLayer
{
    public interface IAccountingSharedDAL 
    {
        Task<long> UpdateAccountName(long accountId, string accountName);
    }
}
