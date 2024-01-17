using Data.Contexts;
using Data.Entities.Purchases;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using DataAccess.Shared.Handlers;
using Microsoft.EntityFrameworkCore;
using Purchases.DataAccessLayer;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataAccessLayer
{
    public class ClientVendorDAL : GenericRepository<ClientVendor>, IClientVendorDAL
    {
        private readonly AppDbContext _appDbContext;
        public ClientVendorDAL(AppDbContext dbContext)
             : base(dbContext)
        {
            _appDbContext = dbContext;
        }

    
     
    }
}
