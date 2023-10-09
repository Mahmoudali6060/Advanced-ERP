using Data.Entities.Setup;
using Shared.DataServiceLayer;
using Shared.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Setup.Contracts
{
    public interface IContactUsDSL :  ICRUDOperationsDSL<ContactUs, ContactUsSearch>
    {

    }
}
