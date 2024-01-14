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
using Shared.Entities.Setup;
using Data.Entities.Setup;
using DataService.Setup.Contracts;

namespace DataService.Setup.Handlers
{
    public class CompanyDSL : ICompanyDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public CompanyDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<CompanyDTO>> GetAll(CompanySearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.CompanyDAL.GetAllAsync();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<CompanyDTO>>(userProfileList);
            return new ResponseEntityList<CompanyDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<CompanyDTO> GetById(long id)
        {
            return _mapper.Map<CompanyDTO>(await _unitOfWork.CompanyDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<CompanyDTO>> GetAllLite()
        {
            return new ResponseEntityList<CompanyDTO>()
            {
                List = _mapper.Map<IEnumerable<CompanyDTO>>(_unitOfWork.CompanyDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.CompanyDAL.GetAllLiteAsync().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(CompanyDTO entityDTO)
        {
            UploadImage(entityDTO);
            var entity = _mapper.Map<Company>(entityDTO);
            await _unitOfWork.CompanyDAL.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(CompanyDTO entity)
        {
            UploadImage(entity);
            var result = await _unitOfWork.CompanyDAL.UpdateAsync(_mapper.Map<Company>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            Company entity = await _unitOfWork.CompanyDAL.GetByIdAsync(id);
            var result = await _unitOfWork.CompanyDAL.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<Company> ApplyFilert(IQueryable<Company> CompanyList, CompanySearchDTO searchCriteriaDTO)
        {
            //Filter
            return CompanyList;
        }

        private bool UploadImage(CompanyDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.Name + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Companies/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }

        #endregion
    }
}
