using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Sales;
using Data.Entities.Sales;
using DataService.Sales.Contracts;
using System;
using Entities.Account;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Shared.Entities.Purchases;

namespace DataService.Sales.Handlers
{
    public class SalesBillHeaderDSL : ISalesBillHeaderDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public SalesBillHeaderDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<SalesBillHeaderDTO>> GetAll(SalesBillHeaderSearchDTO searchCriteriaDTO)
        {
            var salesBillHeaderList = await _unitOfWork.SalesBillHeaderDAL.GetAll();
            int total = salesBillHeaderList.Count();

            #region Apply Filters
            salesBillHeaderList = ApplyFilert(salesBillHeaderList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            salesBillHeaderList = salesBillHeaderList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var salesBillHeaderDTOList = _mapper.Map<IEnumerable<SalesBillHeaderDTO>>(salesBillHeaderList);
            return new ResponseEntityList<SalesBillHeaderDTO>
            {
                List = salesBillHeaderDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<SalesBillHeaderDTO> GetByNumber(string number)
        {
            return _mapper.Map<SalesBillHeaderDTO>(await _unitOfWork.SalesBillHeaderDAL.GetByNumber(number));
        }
        public async Task<SalesBillHeaderDTO> GetById(long id)
        {
            var tt = _mapper.Map<SalesBillHeaderDTO>(await _unitOfWork.SalesBillHeaderDAL.GetById(id));
            return tt;
        }

        public async Task<ResponseEntityList<SalesBillHeaderDTO>> GetAllLite()
        {
            return new ResponseEntityList<SalesBillHeaderDTO>()
            {
                List = _mapper.Map<IEnumerable<SalesBillHeaderDTO>>(_unitOfWork.SalesBillHeaderDAL.GetAllLite().Result),
                Total = _unitOfWork.SalesBillHeaderDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(SalesBillHeaderDTO entity)
        {
            var result = await _unitOfWork.SalesBillHeaderDAL.Add(_mapper.Map<SalesBillHeader>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

    
        public async Task<long> Update(SalesBillHeaderDTO entity)
        {
            var exsitedPurhaseDetails = await _unitOfWork.SalesBillDetailDAL.GetAllByHeaderId(entity.Id);
           
            await _unitOfWork.SalesBillDetailDAL.DeleteRange(exsitedPurhaseDetails.ToList());
            foreach(var item in entity.SalesBillDetailList)
            {
                item.SalesBillHeaderId = entity.Id;
            }
            await _unitOfWork.SalesBillDetailDAL.AddRange(_mapper.Map<List<SalesBillDetail>>(entity.SalesBillDetailList));
            entity.SalesBillDetailList = null;
            var result = await _unitOfWork.SalesBillHeaderDAL.Update(_mapper.Map<SalesBillHeader>(entity));

            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            SalesBillHeader entity = await _unitOfWork.SalesBillHeaderDAL.GetById(id);
            var result = await _unitOfWork.SalesBillHeaderDAL.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<SalesBillHeader> ApplyFilert(IQueryable<SalesBillHeader> salesBillHeaderList, SalesBillHeaderSearchDTO searchCriteriaDTO)
        {

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Number))
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.Number.Contains(searchCriteriaDTO.Number));
            }

            if (searchCriteriaDTO.ClientVendorId.HasValue)
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.ClientVendorId == searchCriteriaDTO.ClientVendorId);
            }
            return salesBillHeaderList;
        }

        #endregion
    }
}
