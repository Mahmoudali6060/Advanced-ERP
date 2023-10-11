using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Setup;
using Data.Entities.Setup;
using DataService.Setup.Contracts;

namespace DataService.Setup.Handlers
{
    public class CityDSL : ICityDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public CityDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<CityDTO>> GetAll(CitySearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.CityDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<CityDTO>>(userProfileList);
            return new ResponseEntityList<CityDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<CityDTO> GetById(long id)
        {
            var test = _mapper.Map<CityDTO>(await _unitOfWork.CityDAL.GetById(id));
            return _mapper.Map<CityDTO>(await _unitOfWork.CityDAL.GetById(id));
        }

        public async Task<ResponseEntityList<CityDTO>> GetAllLite()
        {
            return new ResponseEntityList<CityDTO>()
            {
                List = _mapper.Map<IEnumerable<CityDTO>>(_unitOfWork.CityDAL.GetAllLite().Result),
                Total = _unitOfWork.CityDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<ResponseEntityList<CityDTO>> GetAllLiteByStateId(long stateId)
        {
            return new ResponseEntityList<CityDTO>()
            {
                List = _mapper.Map<IEnumerable<CityDTO>>(_unitOfWork.CityDAL.GetAllLiteByStateId(stateId).Result),
                Total = _unitOfWork.CityDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(CityDTO entity)
        {
            return await _unitOfWork.CityDAL.Add(_mapper.Map<City>(entity));
        }

        public async Task<long> Update(CityDTO entity)
        {
            return await _unitOfWork.CityDAL.Update(_mapper.Map<City>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            City entity = await _unitOfWork.CityDAL.GetById(id);
            return await _unitOfWork.CityDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<City> ApplyFilert(IQueryable<City> CityList, CitySearchDTO searchCriteriaDTO)
        {
            //Filter
            return CityList;
        }

        #endregion
    }
}
