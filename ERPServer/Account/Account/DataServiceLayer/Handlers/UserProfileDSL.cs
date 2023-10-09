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
    public class UserProfileDSL : IUserProfileDSL
    {
        private readonly IUserProfileDAL _userProfileDAL;
        private readonly IAccountDAL _accountDAL;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public UserProfileDSL(IUserProfileDAL userProfileDAL, IAccountDAL accountDAL, IFileManager fileManager, IMapper mapper)
        {
            _userProfileDAL = userProfileDAL;
            _accountDAL = accountDAL;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<ResponseEntityList<UserProfileDTO>> GetAll(UserProfileSearchCriteriaDTO searchCriteriaDTO)
        {
            var userProfileList = await _userProfileDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<UserProfileDTO>>(userProfileList);
            return new ResponseEntityList<UserProfileDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        private IQueryable<UserProfile> ApplyFilert(IQueryable<UserProfile> UserProfileList, UserProfileSearchCriteriaDTO searchCriteriaDTO)
        {
            //Filter
            if (searchCriteriaDTO.UserTypeId > 0)
            {
                UserProfileList = UserProfileList.Where(x => x.UserTypeId == searchCriteriaDTO.UserTypeId);
            }
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.FirstName))
            {
                UserProfileList = UserProfileList.Where(x => x.FirstName.Contains(searchCriteriaDTO.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.LastName))
            {
                UserProfileList = UserProfileList.Where(x => x.LastName.Contains(searchCriteriaDTO.LastName));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Mobile))
            {
                UserProfileList = UserProfileList.Where(x => x.Mobile.Contains(searchCriteriaDTO.Mobile));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.UserName))
            {
                UserProfileList = UserProfileList.Where(x => x.AppUser.UserName.Contains(searchCriteriaDTO.UserName));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.JobTitle))
            {
                UserProfileList = UserProfileList.Where(x => x.JobTitle.Contains(searchCriteriaDTO.JobTitle));
            }
            return UserProfileList;
        }
        public async Task<UserProfileDTO> GetById(long id)
        {
            var test = _mapper.Map<UserProfileDTO>(await _userProfileDAL.GetById(id));
            return _mapper.Map<UserProfileDTO>(await _userProfileDAL.GetById(id));
        }

        public async Task<ResponseEntityList<UserProfileDTO>> GetAllLite()
        {
            return new ResponseEntityList<UserProfileDTO>()
            {
                List = _mapper.Map<IQueryable<UserProfileDTO>>(_userProfileDAL.GetAllLite().Result),
                Total = _userProfileDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<IEnumerable<UserProfileDTO>> GetAllAvailableDrivers(AvailabilitySearchCriteriaDTO availabilitySearchCriteriaDTO)
        {
            var driverList = _userProfileDAL.GetAllDriver().Result;
            driverList = driverList.Where(x =>
            x.UserTypeId == UserTypeEnum.Driver
            && !(x.DriverCarRequests.Count() > 0
            && x.DriverCarRequests.Any(a => a.CarRequestStatusId != CarRequestStatusEnum.Closed && (a.DateFrom.Date >= DateTime.Parse(availabilitySearchCriteriaDTO.DateFrom).Date && a.DateTo.Date <= DateTime.Parse(availabilitySearchCriteriaDTO.DateTo)))));
            IEnumerable<UserProfileDTO> result = _mapper.Map<IEnumerable<UserProfileDTO>>(driverList);
            return result;
        }
        public async Task<IEnumerable<UserProfileDTO>> GetAllDrivers(AvailabilitySearchCriteriaDTO availabilitySearchCriteriaDTO)
        {
            var driverList = _userProfileDAL.GetAllDriver().Result;
            IEnumerable<UserProfileDTO> result = _mapper.Map<IEnumerable<UserProfileDTO>>(driverList);
            return result;
        }

        public async Task<long> Add(UserProfileDTO entity)
        {
            AppUser appUser = UserMapper.MapAppUser(entity);
            string UserType = Enum.GetName(typeof(UserTypeEnum), entity.UserTypeId);
            var createUserResult = await _accountDAL.CreateUserAsync(appUser, entity.Password, UserType);
            if (createUserResult.Succeeded)
            {
                entity.AppUserId = appUser.Id;
                entity.DefaultLanguage = "en";
                UploadImage(entity);
                return await _userProfileDAL.Add(_mapper.Map<UserProfile>(entity));
            }
            await _accountDAL.DeleteUser(appUser.Id);
            throw new Exception(createUserResult.Errors.ToList()[0].Description);
        }

        public async Task<long> Update(UserProfileDTO entity)
        {
            //  UploadImage(entity);
            // AppUser appUser = UserMapper.MapAppUser(entity);
            var createUserResult = await _accountDAL.UpdateUserAsync(entity);
            if (createUserResult.Succeeded)
            {
                UploadImage(entity);
                return await _userProfileDAL.Update(_mapper.Map<UserProfile>(entity));
            }
            throw new Exception(createUserResult.Errors.ToList()[0].Description);
        }

    
        public async Task<bool> Delete(long id)
        {
            UserProfile userProfile = await _userProfileDAL.GetById(id);
            //>>To-Do >> Check this again
            //if (userProfile.AppUserId != null)
            //    await _accountDAL.DeleteUser(userProfile.AppUserId);
            return await _userProfileDAL.Delete(userProfile);
        }

        public async Task<UserProfile> GetUserProfileByAppUserId(string appUserId)
        {
            return await _userProfileDAL.GetUserProfileByAppUserId(appUserId);
        }

        #region Helper Methods
        private bool UploadImage(UserProfileDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.UserName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Users/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }
        #endregion
    }
}
