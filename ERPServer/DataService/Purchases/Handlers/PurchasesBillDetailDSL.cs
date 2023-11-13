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

namespace DataService.Setup.Handlers
{
    public class PurchasesBillDetailDSL : IPurchasesBillDetailDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public PurchasesBillDetailDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<PurchasesBillDetailDTO>> GetAll(PurchasesBillDetailSearchDTO searchCriteriaDTO)
        {
            var purchasesBillDetailList = await _unitOfWork.PurchasesBillDetailDAL.GetAll();
            int total = purchasesBillDetailList.Count();


            #region Apply Pagination
            purchasesBillDetailList = purchasesBillDetailList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var purchasesBillDetailDTOList = _mapper.Map<IEnumerable<PurchasesBillDetailDTO>>(purchasesBillDetailList);
            return new ResponseEntityList<PurchasesBillDetailDTO>
            {
                List = purchasesBillDetailDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<PurchasesBillDetailDTO> GetById(long id)
        {
            var test = _mapper.Map<PurchasesBillDetailDTO>(await _unitOfWork.PurchasesBillDetailDAL.GetById(id));
            return _mapper.Map<PurchasesBillDetailDTO>(await _unitOfWork.PurchasesBillDetailDAL.GetById(id));
        }

        public async Task<ResponseEntityList<PurchasesBillDetailDTO>> GetAllLite()
        {
            return new ResponseEntityList<PurchasesBillDetailDTO>()
            {
                List = _mapper.Map<IEnumerable<PurchasesBillDetailDTO>>(_unitOfWork.PurchasesBillDetailDAL.GetAllLite().Result),
                Total = _unitOfWork.PurchasesBillDetailDAL.GetAllLite().Result.Count()
            };
        }

        
        #endregion

        #region Command
        public async Task<long> Add(PurchasesBillDetailDTO entity)
        {
            var result = await _unitOfWork.PurchasesBillDetailDAL.Add(_mapper.Map<PurchasesBillDetail>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<long> Update(PurchasesBillDetailDTO entity)
        {
            var result= await _unitOfWork.PurchasesBillDetailDAL.Update(_mapper.Map<PurchasesBillDetail>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            PurchasesBillDetail entity = await _unitOfWork.PurchasesBillDetailDAL.GetById(id);
            var result= await _unitOfWork.PurchasesBillDetailDAL.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
       

        #endregion
    }
}
