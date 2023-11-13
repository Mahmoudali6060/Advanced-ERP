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
using Shared.Enums;

namespace DataService.Setup.Handlers
{
    public class ClientVendorDSL : IClientVendorDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public ClientVendorDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<ClientVendorDTO>> GetAll(ClientVendorSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.ClientVendorDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<ClientVendorDTO>>(userProfileList);
            return new ResponseEntityList<ClientVendorDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ClientVendorDTO> GetById(long id)
        {
            return _mapper.Map<ClientVendorDTO>(await _unitOfWork.ClientVendorDAL.GetById(id));
        }

        public async Task<ResponseEntityList<ClientVendorDTO>> GetAllLite()
        {
            return new ResponseEntityList<ClientVendorDTO>()
            {
                List = _mapper.Map<IEnumerable<ClientVendorDTO>>(_unitOfWork.ClientVendorDAL.GetAllLite().Result),
                Total = _unitOfWork.ClientVendorDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<ResponseEntityList<ClientVendorDTO>> GetAllLiteByTypeId(ClientVendorTypeEnum typeId)
        {
            return new ResponseEntityList<ClientVendorDTO>()
            {
                List = _mapper.Map<IEnumerable<ClientVendorDTO>>(_unitOfWork.ClientVendorDAL.GetAllLite().Result.Where(x => x.TypeId == typeId || x.TypeId == ClientVendorTypeEnum.All)),
                Total = _unitOfWork.ClientVendorDAL.GetAllLite().Result.Count(x => x.TypeId == typeId)
            };
        }


        #endregion

        #region Command
        public async Task<long> Add(ClientVendorDTO entity)
        {

            //entity.Code = "CL" + DateTime.Now.ToString("ddMMyyHHmmssff");//ddMMyyHHmmssff
            UploadClientVendorImage(entity);
            var result = await _unitOfWork.ClientVendorDAL.Add(_mapper.Map<ClientVendor>(entity));
            //if (entity.IsVendor)//Update Vendor
            //{
            //    var vendorDTO = MapClientVendorToVendor(entity);
            //    await _unitOfWork.VendorDAL.Add(_mapper.Map<Vendor>(vendorDTO));
            //}
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<long> Update(ClientVendorDTO entity)
        {
            UploadClientVendorImage(entity);
            var result = await _unitOfWork.ClientVendorDAL.Update(_mapper.Map<ClientVendor>(entity));
            //if (entity.IsVendor)//Update Vendor
            //{
            //    var vendorDTO = MapClientVendorToVendor(entity);
            //    UploadVendorImage(vendorDTO);
            //    if (entity.VendorId.HasValue) await _unitOfWork.VendorDAL.Update(_mapper.Map<Vendor>(vendorDTO));
            //    else await _unitOfWork.VendorDAL.Add(_mapper.Map<Vendor>(vendorDTO));
            //}
            //else if (entity.VendorId.HasValue)
            //{
            //    var deletedVendor = await _unitOfWork.VendorDAL.GetById(entity.VendorId.Value);
            //    await _unitOfWork.VendorDAL.Delete(deletedVendor);
            //}
            await _unitOfWork.CompleteAsync();
            return result;
        }


        public async Task<bool> Delete(long id)
        {
            ClientVendor entity = await _unitOfWork.ClientVendorDAL.GetById(id);
            var result = await _unitOfWork.ClientVendorDAL.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<ClientVendor> ApplyFilert(IQueryable<ClientVendor> clientList, ClientVendorSearchDTO searchCriteriaDTO)
        {
            clientList = clientList.Where(x => x.TypeId == searchCriteriaDTO.TypeId || x.TypeId == ClientVendorTypeEnum.All);

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

        private bool UploadClientVendorImage(ClientVendorDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.FullName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/ClientVendors/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }
        #endregion
    }
}
