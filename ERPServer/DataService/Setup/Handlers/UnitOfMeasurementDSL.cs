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
    public class UnitOfMeasurementDSL : IUnitOfMeasurementDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public UnitOfMeasurementDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<UnitOfMeasurementDTO>> GetAll(UnitOfMeasurementSearchDTO searchCriteriaDTO)
        {
            var unitOfMeasurementList = await _unitOfWork.UnitOfMeasurementDAL.GetAllAsync();

            #region Apply Filters
            unitOfMeasurementList = unitOfMeasurementList.OrderBy(x => x.Id);
            unitOfMeasurementList = ApplyFilert(unitOfMeasurementList, searchCriteriaDTO);
            int total = unitOfMeasurementList.Count();
            #endregion

            #region Apply Pagination
            unitOfMeasurementList = unitOfMeasurementList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<UnitOfMeasurementDTO>>(unitOfMeasurementList);
            return new ResponseEntityList<UnitOfMeasurementDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<UnitOfMeasurementDTO> GetById(long id)
        {
            return _mapper.Map<UnitOfMeasurementDTO>(await _unitOfWork.UnitOfMeasurementDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<UnitOfMeasurementDTO>> GetAllLite()
        {
            return new ResponseEntityList<UnitOfMeasurementDTO>()
            {
                List = _mapper.Map<IEnumerable<UnitOfMeasurementDTO>>(_unitOfWork.UnitOfMeasurementDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.UnitOfMeasurementDAL.GetAllLiteAsync().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(UnitOfMeasurementDTO entityDTO)
        {
            var entity = _mapper.Map<UnitOfMeasurement>(entityDTO);
            await _unitOfWork.UnitOfMeasurementDAL.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(UnitOfMeasurementDTO entity)
        {
            var result = await _unitOfWork.UnitOfMeasurementDAL.UpdateAsync(_mapper.Map<UnitOfMeasurement>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            UnitOfMeasurement entity = await _unitOfWork.UnitOfMeasurementDAL.GetByIdAsync(id);
            var result = await _unitOfWork.UnitOfMeasurementDAL.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<UnitOfMeasurement> ApplyFilert(IQueryable<UnitOfMeasurement> UnitOfMeasurementList, UnitOfMeasurementSearchDTO searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                UnitOfMeasurementList = UnitOfMeasurementList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }
            return UnitOfMeasurementList;
        }



        #endregion
    }
}
