using System.Linq;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
using DataAccess.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Setup.DataAccessLayer
{
    public class RepresentiveDAL : GenericRepository<Representive>, IRepresentiveDAL
    {
        public RepresentiveDAL(AppDbContext dbContext)
        : base(dbContext)
        {

        }


    }
}