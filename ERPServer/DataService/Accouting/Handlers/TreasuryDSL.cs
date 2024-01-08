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
using Data.Entities.Accouting;
using Shared.Entities.Accouting;
using DataService.Accounting.Contracts;
using System.Linq.Expressions;

namespace DataService.Accounting.Handlers
{
    public class TreasuryDSL : ITreasuryDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public TreasuryDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<TreasuryDTO>> GetAll(TreasurySearchDTO searchCriteriaDTO)
        {
            var treasuryList = await _unitOfWork.TreasuryDAL.GetAllWithIncludes(x => x.IsCancel == false, x => x.ClientVendor);
            #region Apply Filters
            treasuryList = treasuryList.OrderByDescending(x => x.Id);
            treasuryList = ApplyFilert(treasuryList, searchCriteriaDTO);
            int total = treasuryList.Count();

            #endregion

            #region Apply Pagination
            treasuryList = treasuryList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var treasuryDTOList = _mapper.Map<IEnumerable<TreasuryDTO>>(treasuryList);
            return new ResponseEntityList<TreasuryDTO>
            {
                List = treasuryDTOList,
                Total = total,
            };
            #endregion

        }

        public async Task<TreasuryGridDTO> GetAllForGrid(TreasurySearchDTO searchCriteriaDTO)
        {
            var treasuryList = await _unitOfWork.TreasuryDAL.GetAllWithIncludes(x => x.IsCancel == false, x => x.ClientVendor);
            #region Apply Filters
            treasuryList = treasuryList.OrderByDescending(x => x.Id);
            treasuryList = ApplyFilert(treasuryList, searchCriteriaDTO);
            int total = treasuryList.Count();
            decimal balance = treasuryList.Sum(x => x.Debit - x.Credit);
            #endregion

            #region Apply Pagination
            treasuryList = treasuryList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var treasuryDTOList = _mapper.Map<IEnumerable<TreasuryDTO>>(treasuryList);
            return new TreasuryGridDTO
            {
                List = treasuryDTOList,
                Total = total,
                Balance = balance
            };
            #endregion

        }

        public async Task<TreasuryDTO> GetById(long id)
        {
            return _mapper.Map<TreasuryDTO>(await _unitOfWork.TreasuryDAL.GetById(id));
        }

        public async Task<ResponseEntityList<TreasuryDTO>> GetAllLite()
        {
            return new ResponseEntityList<TreasuryDTO>()
            {
                List = _mapper.Map<IEnumerable<TreasuryDTO>>(_unitOfWork.TreasuryDAL.GetAllLite().Result),
                Total = _unitOfWork.TreasuryDAL.GetAllLite().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(TreasuryDTO entityDTO)
        {
            var entity = _mapper.Map<Treasury>(entityDTO);
            await _unitOfWork.TreasuryDAL.Add(entity);
            #region Update Balance
            if (entityDTO.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entityDTO.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.Debit;
                    clientVendor.Credit += entity.Credit;
                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            #endregion

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(TreasuryDTO entity)
        {
            var oldEntity = await _unitOfWork.TreasuryDAL.GetById(entity.Id);
            var result = await _unitOfWork.TreasuryDAL.Update(_mapper.Map<Treasury>(entity));
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.Debit - oldEntity.Debit;
                    clientVendor.Credit += entity.Credit - oldEntity.Credit;
                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            else if (oldEntity.ClientVendorId.HasValue)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(oldEntity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    //clientVendor.Debit -= entity.Amount;
                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            Treasury entity = await _unitOfWork.TreasuryDAL.GetById(id);
            var result = await _unitOfWork.TreasuryDAL.Delete(entity);
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit -= entity.Debit;
                    clientVendor.Credit -= entity.Credit;
                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<Treasury> ApplyFilert(IQueryable<Treasury> TreasuryList, TreasurySearchDTO searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Date))
            {
                TreasuryList = TreasuryList.Where(x => x.Date.Date == DateTime.Parse(searchCriteriaDTO.Date));
            }

            if (searchCriteriaDTO.AccountTypeId.HasValue)
            {
                TreasuryList = TreasuryList.Where(x => x.AccountTypeId == searchCriteriaDTO.AccountTypeId);
            }

            if (searchCriteriaDTO.ClientVendorId.HasValue)
            {
                TreasuryList = TreasuryList.Where(x => x.ClientVendorId == searchCriteriaDTO.ClientVendorId);
            }



            if (searchCriteriaDTO.PaymentMethodId.HasValue)
            {
                TreasuryList = TreasuryList.Where(x => x.PaymentMethodId == searchCriteriaDTO.PaymentMethodId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.RefNo))
            {
                TreasuryList = TreasuryList.Where(x => x.RefNo == searchCriteriaDTO.RefNo);
            }

            return TreasuryList;
        }



        #endregion
    }
}
