using Data.Contexts;
using Data.Entities.Accouting;
using DataAccess.Accounting.Contracts;
using DataAccess.Shared.Handlers;

namespace Accounting.DataAccessLayer
{
    public class AccountStatementDAL : GenericRepository<AccountStatement>, IAccountStatementDAL
    {
        public AccountStatementDAL(AppDbContext dbContext)
        : base(dbContext)
        {

        }


    }
}