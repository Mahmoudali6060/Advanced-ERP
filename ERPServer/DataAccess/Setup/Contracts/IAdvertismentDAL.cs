using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using Shared.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IAdvertismentDAL : ICRUDOperationsAsyncDAL<Advertisment>
    {
        Task<long> AddRang(List<Advertisment> lstAdvertisments);
    }
}
