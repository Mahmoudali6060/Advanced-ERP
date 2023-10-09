using Data.Entities.Setup;
using Shared.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IAboutUsDAL : ICRUDOperationsDAL<AboutUs>
    {
    }
}
