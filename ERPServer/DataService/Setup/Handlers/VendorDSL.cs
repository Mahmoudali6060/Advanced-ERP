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
    public class VendorDSL : IVendorDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public VendorDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<VendorDTO>> GetAll(VendorSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.VendorDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<VendorDTO>>(userProfileList);
            return new ResponseEntityList<VendorDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<VendorDTO> GetById(long id)
        {
            var test = _mapper.Map<VendorDTO>(await _unitOfWork.VendorDAL.GetById(id));
            return _mapper.Map<VendorDTO>(await _unitOfWork.VendorDAL.GetById(id));
        }

        public async Task<ResponseEntityList<VendorDTO>> GetAllLite()
        {
            return new ResponseEntityList<VendorDTO>()
            {
                List = _mapper.Map<IEnumerable<VendorDTO>>(_unitOfWork.VendorDAL.GetAllLite().Result),
                Total = _unitOfWork.VendorDAL.GetAllLite().Result.Count()
            };
        }

        
        #endregion

        #region Command
        public async Task<long> Add(VendorDTO entity)
        {
            UploadImage(entity);
            var result = await _unitOfWork.VendorDAL.Add(_mapper.Map<Vendor>(entity));
            return result;
        }

        public async Task<long> Update(VendorDTO entity)
        {
            UploadImage(entity);
            return await _unitOfWork.VendorDAL.Update(_mapper.Map<Vendor>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            Vendor entity = await _unitOfWork.VendorDAL.GetById(id);
            return await _unitOfWork.VendorDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<Vendor> ApplyFilert(IQueryable<Vendor> vendorList, VendorSearchDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.IsActive.HasValue)
            {
                vendorList = vendorList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                vendorList = vendorList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.FullName))
            {
                vendorList = vendorList.Where(x => x.FullName.Contains(searchCriteriaDTO.FullName));
            }
            return vendorList;
        }

        private bool UploadImage(VendorDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.FullName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Vendors/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }
        #endregion
    }
}
