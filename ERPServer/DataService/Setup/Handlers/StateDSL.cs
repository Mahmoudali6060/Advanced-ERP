using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Setup;
using Data.Entities.Setup;

namespace Setup.DataServiceLayer
{
    public class StateDSL : IStateDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public StateDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<StateDTO>> GetAll(StateSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.StateDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<StateDTO>>(userProfileList);
            return new ResponseEntityList<StateDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<StateDTO> GetById(long id)
        {
            var test = _mapper.Map<StateDTO>(await _unitOfWork.StateDAL.GetById(id));
            return _mapper.Map<StateDTO>(await _unitOfWork.StateDAL.GetById(id));
        }

        public async Task<ResponseEntityList<StateDTO>> GetAllLite()
        {
            return new ResponseEntityList<StateDTO>()
            {
                List = _mapper.Map<IEnumerable<StateDTO>>(_unitOfWork.StateDAL.GetAllLite().Result),
                Total = _unitOfWork.StateDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<ResponseEntityList<StateDTO>> GetAllLiteByCountryId(long countryId)
        {
            return new ResponseEntityList<StateDTO>()
            {
                List = _mapper.Map<IEnumerable<StateDTO>>(_unitOfWork.StateDAL.GetAllLiteByCountryId(countryId).Result),
                Total = _unitOfWork.StateDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(StateDTO entity)
        {
            return await _unitOfWork.StateDAL.Add(_mapper.Map<State>(entity));
        }

        public async Task<long> Update(StateDTO entity)
        {
            return await _unitOfWork.StateDAL.Update(_mapper.Map<State>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            State entity = await _unitOfWork.StateDAL.GetById(id);
            return await _unitOfWork.StateDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<State> ApplyFilert(IQueryable<State> StateList, StateSearchDTO searchCriteriaDTO)
        {
            //Filter
            return StateList;
        }

        #endregion
    }
}
