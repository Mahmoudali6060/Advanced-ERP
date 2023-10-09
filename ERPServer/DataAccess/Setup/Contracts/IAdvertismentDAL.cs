using Data.Entities.Setup;
using Shared.DataAccessLayer;
using Shared.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IAdvertismentDAL : ICRUDOperationsDAL<Advertisment>
    {
        Task<long> AddRang(List<Advertisment> lstAdvertisments);
    }
}
