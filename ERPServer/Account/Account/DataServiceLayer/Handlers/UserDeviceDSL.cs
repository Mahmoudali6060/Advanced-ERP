using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Shared.Classes;
using Account.DataAccessLayer;
using Account.Entities;
using Data.Entities.UserManagement;
using Account.RepositoryLayer;
using Infrastructure.Contracts;
using Shared.Enums;

namespace Accout.DataServiceLayer
{
    public class UserDeviceDSL : IUserDeviceDSL
    {
        private readonly IUserDeviceDAL _userProfileDAL;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public UserDeviceDSL(IUserDeviceDAL userProfileDAL, IFileManager fileManager, IMapper mapper)
        {
            _userProfileDAL = userProfileDAL;
            _fileManager = fileManager;
            _mapper = mapper;
        }


        public async Task<long> SetDeviceId(UserProfileDTO entity)
        {
            UserDevice userDevice = await _userProfileDAL.GetUserDeviceByUserProfileId(entity.Id);
          
            if (userDevice != null)
            {
                userDevice.DeviceId = entity.DeviceId;
                return await _userProfileDAL.Update(userDevice);
            }

            userDevice = new UserDevice()
            {
                UserProfileId = entity.Id,
                DeviceId = entity.DeviceId
            };

            return await _userProfileDAL.Add(userDevice);
        }


        #region Helper Methods
        #endregion
    }
}
