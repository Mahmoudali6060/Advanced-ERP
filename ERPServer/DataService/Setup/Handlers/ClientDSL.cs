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
using System;
using Entities.Account;

namespace DataService.Setup.Handlers
{
    public class ClientDSL : IClientDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public ClientDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<ClientDTO>> GetAll(ClientSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.ClientDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<ClientDTO>>(userProfileList);
            return new ResponseEntityList<ClientDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ClientDTO> GetById(long id)
        {
            var test = _mapper.Map<ClientDTO>(await _unitOfWork.ClientDAL.GetById(id));
            return _mapper.Map<ClientDTO>(await _unitOfWork.ClientDAL.GetById(id));
        }

        public async Task<ResponseEntityList<ClientDTO>> GetAllLite()
        {
            return new ResponseEntityList<ClientDTO>()
            {
                List = _mapper.Map<IEnumerable<ClientDTO>>(_unitOfWork.ClientDAL.GetAllLite().Result),
                Total = _unitOfWork.ClientDAL.GetAllLite().Result.Count()
            };
        }

        
        #endregion

        #region Command
        public async Task<long> Add(ClientDTO entity)
        {
            //entity.Code = "CL" + DateTime.Now.ToString("ddMMyyHHmmssff");//ddMMyyHHmmssff
            UploadImage(entity);
            var result = await _unitOfWork.ClientDAL.Add(_mapper.Map<Client>(entity));
            return result;
        }

        public async Task<long> Update(ClientDTO entity)
        {
            UploadImage(entity);
            return await _unitOfWork.ClientDAL.Update(_mapper.Map<Client>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            Client entity = await _unitOfWork.ClientDAL.GetById(id);
            return await _unitOfWork.ClientDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<Client> ApplyFilert(IQueryable<Client> clientList, ClientSearchDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.IsActive.HasValue)
            {
                clientList = clientList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                clientList = clientList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.FullName))
            {
                clientList = clientList.Where(x => x.FullName.Contains(searchCriteriaDTO.FullName));
            }
            return clientList;
        }

        private bool UploadImage(ClientDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.FullName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Clients/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }
        #endregion
    }
}
