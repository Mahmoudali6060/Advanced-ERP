using Data.Entities.Setup;
using Shared.DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Setup.Contracts
{
    public interface IContactUsDAL : ICRUDOperationsAsyncDAL<ContactUs>
    {
    }
}
