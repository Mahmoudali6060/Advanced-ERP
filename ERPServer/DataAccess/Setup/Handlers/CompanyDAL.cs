using System.Linq;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
using DataAccess.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Setup.DataAccessLayer
{
    public class CompanyDAL : GenericRepository<Company>, ICompanyDAL
    {
        public CompanyDAL(AppDbContext dbContext)
        : base(dbContext)
        {
            
        }

      
    }
}