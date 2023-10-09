using Account.Entities;
using Data.Entities.UserManagement;
using Shared.DataServiceLayer;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accout.DataServiceLayer
{
    public interface IUserDeviceDSL
    {
        Task<long> SetDeviceId(UserProfileDTO userProfileDTO);
    }
}
