

using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Shared.Entities.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setting.DataServiceLayer
{
    public interface ISettingDSL
    {
        List<Settings> GetSettings();
    }
}
