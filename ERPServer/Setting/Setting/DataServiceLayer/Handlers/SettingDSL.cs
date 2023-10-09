using Data.Constants;
using Data.Contexts;
using Data.Entities;
using Data.Entities.Shared;
using Setting.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setting.DataServiceLayer
{
    public class SettingDSL : ISettingDSL
    {
        ISettingDAL _settingDAL;
        public SettingDSL(ISettingDAL settingDAL)
        {
            _settingDAL = settingDAL;
        }

        public List<Settings> GetSettings()
        {
            return _settingDAL.GetSettings().ToList();
        }
    }
}
