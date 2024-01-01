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
    public class RepresentiveDSL : IRepresentiveDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public RepresentiveDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<RepresentiveDTO>> GetAll(RepresentiveSearchDTO searchCriteriaDTO)
        {
            var representiveList = await _unitOfWork.RepresentiveDAL.GetAll();

            #region Apply Filters
            representiveList = representiveList.OrderByDescending(x => x.Id);
            representiveList = ApplyFilert(representiveList, searchCriteriaDTO);
            int total = representiveList.Count();

            #endregion

            #region Apply Pagination
            representiveList = representiveList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<RepresentiveDTO>>(representiveList);
            return new ResponseEntityList<RepresentiveDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<RepresentiveDTO> GetById(long id)
        {
            return _mapper.Map<RepresentiveDTO>(await _unitOfWork.RepresentiveDAL.GetById(id));
        }

        public async Task<ResponseEntityList<RepresentiveDTO>> GetAllLite()
        {
            return new ResponseEntityList<RepresentiveDTO>()
            {
                List = _mapper.Map<IEnumerable<RepresentiveDTO>>(_unitOfWork.RepresentiveDAL.GetAllLite().Result),
                Total = _unitOfWork.RepresentiveDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(RepresentiveDTO entityDTO)
        {
            var entity = _mapper.Map<Representive>(entityDTO);
            await _unitOfWork.RepresentiveDAL.Add(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(RepresentiveDTO entity)
        {
            var result = await _unitOfWork.RepresentiveDAL.Update(_mapper.Map<Representive>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            Representive entity = await _unitOfWork.RepresentiveDAL.GetById(id);
            var result = await _unitOfWork.RepresentiveDAL.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<Representive> ApplyFilert(IQueryable<Representive> RepresentiveList, RepresentiveSearchDTO searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.FullName))
            {
                RepresentiveList = RepresentiveList.Where(x => x.FullName.Contains(searchCriteriaDTO.FullName));
            }

            if (searchCriteriaDTO.RepresentiveTypeId.HasValue)
            {
                RepresentiveList = RepresentiveList.Where(x => x.RepresentiveTypeId == searchCriteriaDTO.RepresentiveTypeId);
            }

            return RepresentiveList;
        }



        #endregion
    }
}
