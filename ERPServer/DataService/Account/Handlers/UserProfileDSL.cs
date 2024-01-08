using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using Shared.Enums;
using UnitOfWork.Contracts;
using Entities.Account;
using Data.Entities.UserManagement;
using Account.Helpers;
using Data.Entities.Shared;
using Shared.Entities.Setup;

namespace Accout.DataServiceLayer
{
    public class UserProfileDSL : IUserProfileDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public UserProfileDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<ResponseEntityList<UserProfileDTO>> GetAll(UserProfileSearchCriteriaDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.UserProfileDAL.GetAll();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            int total = userProfileList.Count();
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

            if (searchCriteriaDTO.IsActive.HasValue)
            {
                UserProfileList = UserProfileList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }
            return UserProfileList;
        }
        public async Task<UserProfileDTO> GetById(long id)
        {
            UserProfileDTO userProfileDTO = new UserProfileDTO();
            var userProfile = await _unitOfWork.UserProfileDAL.GetById(id);
            userProfileDTO = _mapper.Map<UserProfileDTO>(userProfile);
            if (userProfile.Company != null)
            {
                userProfileDTO.CompanyDTO = _mapper.Map<CompanyDTO>(userProfile.Company);
            }
            return userProfileDTO;
        }

        public async Task<ResponseEntityList<UserProfileDTO>> GetAllLite()
        {
            return new ResponseEntityList<UserProfileDTO>()
            {
                List = _mapper.Map<IQueryable<UserProfileDTO>>(_unitOfWork.UserProfileDAL.GetAllLite().Result),
                Total = _unitOfWork.UserProfileDAL.GetAllLite().Result.Count()
            };
        }

        private bool IsExistUser(UserProfileDTO userProfile)
        {
            var existUsers = _unitOfWork.UserProfileDAL.GetAllLite().Result;
            if (!string.IsNullOrWhiteSpace(userProfile.UserName))
            {
                if (existUsers.Any(x => x.AppUser.UserName == userProfile.UserName && x.Id != userProfile.Id))
                {
                    throw new Exception("Errors.UserNameExistPleaseChooseAnotherUserName");
                }
            }
            if (!string.IsNullOrWhiteSpace(userProfile.Email))
            {
                if (existUsers.Any(x => x.AppUser.Email == userProfile.Email && x.Id != userProfile.Id))
                {
                    throw new Exception("Errors.EmailExistPleaseChooseAnotherEmail");
                }
            }
            if (!string.IsNullOrWhiteSpace(userProfile.Mobile))
            {
                if (existUsers.Any(x => x.Mobile == userProfile.Mobile && x.Id != userProfile.Id))
                {
                    throw new Exception("Errors.MobileExistPleaseChooseAnotherMobile");
                }
            }
            return false;
        }

        public async Task<long> Add(UserProfileDTO entity)
        {
            if (!IsExistUser(entity))
            {
                entity.IsActive = true;
                entity.IsFirstLogin = true;
                entity.IsHide = false;
                AppUser appUser = UserMapper.MapAppUser(entity);
                var createUserResult = await _unitOfWork.AccountDAL.CreateUserAsync(appUser, entity.Password);
                if (createUserResult.Succeeded)
                {
                    entity.AppUserId = appUser.Id;
                    entity.DefaultLanguage = "en";
                    UploadImage(entity);
                    var result = await _unitOfWork.UserProfileDAL.Add(_mapper.Map<UserProfile>(entity));
                    await _unitOfWork.CompleteAsync();
                    return result;
                }
                await _unitOfWork.AccountDAL.DeleteUser(appUser.Id);
                await _unitOfWork.CompleteAsync();
                throw new Exception(createUserResult.Errors.ToList()[0].Description);
            }
            throw new Exception("Errors.InvalidData");
        }

        public async Task<long> Update(UserProfileDTO entity)
        {
            //  UploadImage(entity);
            AppUser appUser = UserMapper.MapAppUser(entity);
            if (!IsExistUser(entity))
            {
                var createUserResult = await _unitOfWork.AccountDAL.UpdateUserAsync(entity);
                if (createUserResult.Succeeded)
                {
                    UploadImage(entity);
                    if ((entity.CompanyId != 0 || entity.CompanyId != null) && entity.CompanyDTO != null)
                    {
                        var company = UserMapper.MapCompany(entity);
                    }
                    var result = await _unitOfWork.UserProfileDAL.Update(_mapper.Map<UserProfile>(entity));
                    await _unitOfWork.CompleteAsync();
                    return result;
                }
                throw new Exception(createUserResult.Errors.ToList()[0].Description);
            }
            throw new Exception("Errors.InvalidData");
        }


        public async Task<bool> Delete(long id)
        {
            try
            {
                UserProfile userProfile = await _unitOfWork.UserProfileDAL.GetById(id);
                //>>To-Do >> Check this again
                if (userProfile.AppUserId != null)
                    await _unitOfWork.UserProfileDAL.Delete(userProfile);
                var result = await _unitOfWork.AccountDAL.DeleteUser(userProfile.AppUserId);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Errors.CannotDeleteThisRecordDueToRelatedToAnotherData");
            }

        }

        public async Task<UserProfile> GetUserProfileByAppUserId(string appUserId)
        {
            return await _unitOfWork.UserProfileDAL.GetUserProfileByAppUserId(appUserId);
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
