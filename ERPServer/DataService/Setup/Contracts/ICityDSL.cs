using Data.Entities.Setup;
using Entities.Account;
using IdentityModel;
using Shared.DataServiceLayer;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setup.DataServiceLayer
{
    public interface ICityDSL : ICRUDOperationsDSL<CityDTO, CitySearchDTO>
    {
        Task<ResponseEntityList<CityDTO>> GetAllLiteByStateId(long stateId);

    }
}
