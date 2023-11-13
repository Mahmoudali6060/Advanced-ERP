using Data.Entities.Setup;
using Data.Entities.Shared;
using Entities.Account;
using IdentityModel;
using Shared.DataServiceLayer;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Setup.Contracts
{
    public interface IProductDSL : ICRUDOperationsDSL<ProductDTO, ProductSearchDTO>
    {
        Task<ResponseEntityList<ProductDTO>> GetAllLiteByCategoryId(long categoryId);
        Task<bool> UpdateAll(List<ProductDTO> entitylist);

    }
}
