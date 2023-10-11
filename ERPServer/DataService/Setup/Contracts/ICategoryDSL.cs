using Shared.DataServiceLayer;
using Shared.Entities.Setup;

namespace DataService.Setup.Contracts
{
    public interface ICategoryDSL : ICRUDOperationsDSL<CategoryDTO, CategorySearchDTO>
    {

    }
}
