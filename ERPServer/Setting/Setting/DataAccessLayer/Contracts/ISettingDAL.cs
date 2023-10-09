

using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Shared.Entities.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setting.DataAccessLayer
{
    public interface ISettingDAL
    {
        IEnumerable<Settings> GetSettings();
    }
}
