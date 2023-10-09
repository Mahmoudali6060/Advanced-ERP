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

namespace Setup.DataServiceLayer
{
    public class CountryDSL : ICountryDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public CountryDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<CountryDTO>> GetAll(CountrySearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.CountryDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<CountryDTO>>(userProfileList);
            return new ResponseEntityList<CountryDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<CountryDTO> GetById(long id)
        {
            var test = _mapper.Map<CountryDTO>(await _unitOfWork.CountryDAL.GetById(id));
            return _mapper.Map<CountryDTO>(await _unitOfWork.CountryDAL.GetById(id));
        }

        public async Task<ResponseEntityList<CountryDTO>> GetAllLite()
        {
            return new ResponseEntityList<CountryDTO>()
            {
                List = _mapper.Map<IEnumerable<CountryDTO>>(_unitOfWork.CountryDAL.GetAllLite().Result),
                Total = _unitOfWork.CountryDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(CountryDTO entity)
        {
            return await _unitOfWork.CountryDAL.Add(_mapper.Map<Country>(entity));
        }

        public async Task<long> Update(CountryDTO entity)
        {
            return await _unitOfWork.CountryDAL.Update(_mapper.Map<Country>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            Country entity = await _unitOfWork.CountryDAL.GetById(id);
            return await _unitOfWork.CountryDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<Country> ApplyFilert(IQueryable<Country> CountryList, CountrySearchDTO searchCriteriaDTO)
        {
            //Filter
            return CountryList;
        }

        #endregion
    }
}
