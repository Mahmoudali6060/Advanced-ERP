using Data.Entities.Setup;
using Entities.Account;
using IdentityModel;
using Shared.DataServiceLayer;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Setup.Contracts
{
    public interface IClientVendorDSL : ICRUDOperationsDSL<ClientVendorDTO, ClientVendorSearchDTO>
    {
        Task<ResponseEntityList<ClientVendorDTO>> GetAllLiteByTypeId(ClientVendorTypeEnum typeId);

    }
}
