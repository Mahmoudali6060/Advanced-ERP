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

namespace DataService.Sales.Handlers
{
    public class SalesBillDetailDSL : ISalesBillDetailDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public SalesBillDetailDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<SalesBillDetailDTO>> GetAll(SalesBillDetailSearchDTO searchCriteriaDTO)
        {
            var salesBillDetailList = await _unitOfWork.SalesBillDetailDAL.GetAllAsync();
            int total = salesBillDetailList.Count();


            #region Apply Pagination
            salesBillDetailList = salesBillDetailList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var salesBillDetailDTOList = _mapper.Map<IEnumerable<SalesBillDetailDTO>>(salesBillDetailList);
            return new ResponseEntityList<SalesBillDetailDTO>
            {
                List = salesBillDetailDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<SalesBillDetailDTO> GetById(long id)
        {
            var test = _mapper.Map<SalesBillDetailDTO>(await _unitOfWork.SalesBillDetailDAL.GetByIdAsync(id));
            return _mapper.Map<SalesBillDetailDTO>(await _unitOfWork.SalesBillDetailDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<SalesBillDetailDTO>> GetAllLite()
        {
            return new ResponseEntityList<SalesBillDetailDTO>()
            {
                List = _mapper.Map<IEnumerable<SalesBillDetailDTO>>(_unitOfWork.SalesBillDetailDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.SalesBillDetailDAL.GetAllLiteAsync().Result.Count()
            };
        }


        #endregion

        #region Command
        public async Task<long> Add(SalesBillDetailDTO entity)
        {
            var result = await _unitOfWork.SalesBillDetailDAL.AddAsync(_mapper.Map<SalesBillDetail>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<long> Update(SalesBillDetailDTO entity)
        {
            var result = await _unitOfWork.SalesBillDetailDAL.UpdateAsync(_mapper.Map<SalesBillDetail>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            SalesBillDetail entity = await _unitOfWork.SalesBillDetailDAL.GetByIdAsync(id);
            var result = await _unitOfWork.SalesBillDetailDAL.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods


        #endregion
    }
}
