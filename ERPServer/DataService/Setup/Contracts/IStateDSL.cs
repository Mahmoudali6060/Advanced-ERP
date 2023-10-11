using Data.Entities.Setup;
using Entities.Account;
using Shared.DataServiceLayer;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Setup.Contracts
{
    public interface IStateDSL : ICRUDOperationsDSL<StateDTO, StateSearchDTO>
    {
        Task<ResponseEntityList<StateDTO>> GetAllLiteByCountryId(long countryId);
    }
}
