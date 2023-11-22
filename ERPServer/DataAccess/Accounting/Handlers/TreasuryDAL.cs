using Data.Contexts;
using Data.Entities.Accouting;
using DataAccess.Accounting.Contracts;
using DataAccess.Shared.Handlers;

namespace Accounting.DataAccessLayer
{
    public class TreasuryDAL : GenericRepository<Treasury>, ITreasuryDAL
    {
        public TreasuryDAL(AppDbContext dbContext)
        : base(dbContext)
        {

        }


    }
}