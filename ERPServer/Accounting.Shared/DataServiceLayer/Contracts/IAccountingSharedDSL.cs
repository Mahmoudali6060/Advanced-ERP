
using System.Threading.Tasks;

namespace Accouting.Shared.DataServiceLayer
{
    public interface IAccountingSharedDSL
    {
        Task<long> UpdateAccountName(long accountId, string accountName);
    }
}
