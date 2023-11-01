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
    public class RoleDSL : IRoleDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public RoleDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<ResponseEntityList<RoleGroupDTO>> GetAll(RoleGroupSearchDTO searchCriteriaDTO)
        {
            var roleGroupList = await _unitOfWork.RoleDAL.GetAll();
            int total = roleGroupList.Count();

            #region Apply Filters
            roleGroupList = ApplyFilert(roleGroupList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            roleGroupList = roleGroupList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var roleGroupDTOList = _mapper.Map<IEnumerable<RoleGroupDTO>>(roleGroupList);
            return new ResponseEntityList<RoleGroupDTO>
            {
                List = roleGroupDTOList,
                Total = total
            };
            #endregion

        }

        private IQueryable<RoleGroup> ApplyFilert(IQueryable<RoleGroup> roleGroupList, RoleGroupSearchDTO searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                roleGroupList = roleGroupList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }


            return roleGroupList;
        }
        public async Task<RoleGroupDTO> GetById(long id)
        {
            var roleGroup = await _unitOfWork.RoleDAL.GetById(id);
            return _mapper.Map<RoleGroupDTO>(roleGroup);
        }

        public async Task<ResponseEntityList<RoleGroupDTO>> GetAllLite()
        {
            var roleList = _unitOfWork.RoleDAL.GetAllLite().Result;
            return new ResponseEntityList<RoleGroupDTO>()
            {
                List = _mapper.Map<IEnumerable<RoleGroupDTO>>(roleList),
                Total = roleList.Count()
            };
        }


        public async Task<long> Add(RoleGroupDTO entity)
        {
            var result= await _unitOfWork.RoleDAL.Add(_mapper.Map<RoleGroup>(entity));
            await _unitOfWork.CompleteAsync();
            return result;

        }

        public async Task<long> Update(RoleGroupDTO entity)
        {
            return await _unitOfWork.RoleDAL.Update(_mapper.Map<RoleGroup>(entity));
        }


        public async Task<bool> Delete(long id)
        {
            RoleGroup roleGroup = await _unitOfWork.RoleDAL.GetById(id);
            return await _unitOfWork.RoleDAL.Delete(roleGroup);
        }

    }
}
