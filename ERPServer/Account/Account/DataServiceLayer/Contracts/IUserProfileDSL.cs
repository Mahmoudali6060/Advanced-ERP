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
    public interface IUserProfileDSL : ICRUDOperationsDSL<UserProfileDTO, UserProfileSearchCriteriaDTO>
    {
        Task<UserProfile> GetUserProfileByAppUserId(string appUserId);
        Task<IEnumerable<UserProfileDTO>> GetAllAvailableDrivers(AvailabilitySearchCriteriaDTO availabilitySearchCriteriaDTO);
        Task<IEnumerable<UserProfileDTO>> GetAllDrivers(AvailabilitySearchCriteriaDTO availabilitySearchCriteriaDTO);


    }
}
