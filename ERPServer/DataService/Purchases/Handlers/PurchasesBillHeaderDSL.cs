using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Purchases;
using Data.Entities.Purchases;
using DataService.Purchases.Contracts;
using System;
using Entities.Account;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace DataService.Setup.Handlers
{
    public class PurchasesBillHeaderDSL : IPurchasesBillHeaderDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public PurchasesBillHeaderDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<PurchasesBillHeaderDTO>> GetAll(PurchasesBillHeaderSearchDTO searchCriteriaDTO)
        {
            var purchasesBillHeaderList = await _unitOfWork.PurchasesBillHeaderDAL.GetAll();
            int total = purchasesBillHeaderList.Count();

            #region Apply Filters
            purchasesBillHeaderList = ApplyFilert(purchasesBillHeaderList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            purchasesBillHeaderList = purchasesBillHeaderList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var purchasesBillHeaderDTOList = _mapper.Map<IEnumerable<PurchasesBillHeaderDTO>>(purchasesBillHeaderList);
            return new ResponseEntityList<PurchasesBillHeaderDTO>
            {
                List = purchasesBillHeaderDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<PurchasesBillHeaderDTO> GetById(long id)
        {
            var tt = _mapper.Map<PurchasesBillHeaderDTO>(await _unitOfWork.PurchasesBillHeaderDAL.GetById(id));
            return tt;
        }

        public async Task<ResponseEntityList<PurchasesBillHeaderDTO>> GetAllLite()
        {
            return new ResponseEntityList<PurchasesBillHeaderDTO>()
            {
                List = _mapper.Map<IEnumerable<PurchasesBillHeaderDTO>>(_unitOfWork.PurchasesBillHeaderDAL.GetAllLite().Result),
                Total = _unitOfWork.PurchasesBillHeaderDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(PurchasesBillHeaderDTO entity)
        {
            var result = await _unitOfWork.PurchasesBillHeaderDAL.Add(_mapper.Map<PurchasesBillHeader>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

    
        public async Task<long> Update(PurchasesBillHeaderDTO entity)
        {
            var exsitedPurhaseDetails = await _unitOfWork.PurchasesBillDetailDAL.GetAllByHeaderId(entity.Id);
           
            await _unitOfWork.PurchasesBillDetailDAL.DeleteRange(exsitedPurhaseDetails.ToList());
            foreach(var item in entity.PurchasesBillDetailList)
            {
                item.PurchasesBillHeaderId = entity.Id;
            }
            await _unitOfWork.PurchasesBillDetailDAL.AddRange(_mapper.Map<List<PurchasesBillDetail>>(entity.PurchasesBillDetailList));
            entity.PurchasesBillDetailList = null;
            var result = await _unitOfWork.PurchasesBillHeaderDAL.Update(_mapper.Map<PurchasesBillHeader>(entity));

            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            PurchasesBillHeader entity = await _unitOfWork.PurchasesBillHeaderDAL.GetById(id);
            var result = await _unitOfWork.PurchasesBillHeaderDAL.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<PurchasesBillHeader> ApplyFilert(IQueryable<PurchasesBillHeader> purchasesBillHeaderList, PurchasesBillHeaderSearchDTO searchCriteriaDTO)
        {

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Number))
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.Number.Contains(searchCriteriaDTO.Number));
            }

            if (searchCriteriaDTO.VendorId.HasValue)
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.VendorId == searchCriteriaDTO.VendorId);
            }
            return purchasesBillHeaderList;
        }

        #endregion
    }
}
