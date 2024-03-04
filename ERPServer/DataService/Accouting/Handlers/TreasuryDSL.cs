﻿using AutoMapper;
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
using Data.Contexts;

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
            var treasuryList = await _unitOfWork.TreasuryDAL.GetAsync(x => x.IsCancel == false, x => x.ClientVendor);
            #region Apply Filters
            treasuryList = treasuryList.OrderBy(x => x.Id);
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
            var treasuryList = await _unitOfWork.TreasuryDAL.GetAsync(x => x.IsCancel == false, x => x.ClientVendor);
            #region Apply Filters
            treasuryList = treasuryList.OrderBy(x => x.Id);
            treasuryList = ApplyFilert(treasuryList, searchCriteriaDTO);
            int total = treasuryList.Count();
            decimal balance = treasuryList.Sum(x => x.InComing - x.OutComing);
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
            return _mapper.Map<TreasuryDTO>(await _unitOfWork.TreasuryDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<TreasuryDTO>> GetAllLite()
        {
            return new ResponseEntityList<TreasuryDTO>()
            {
                List = _mapper.Map<IEnumerable<TreasuryDTO>>(_unitOfWork.TreasuryDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.TreasuryDAL.GetAllLiteAsync().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(TreasuryDTO entityDTO)
        {
            entityDTO.Number = GenerateSequenceNumber();
            var entity = _mapper.Map<Treasury>(entityDTO);
            if (entityDTO.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entityDTO.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.InComing;
                    clientVendor.Credit += entity.OutComing;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }

                entity.AccountStatements = MapAccountStatement(entityDTO);
            }
            await _unitOfWork.TreasuryDAL.AddAsync(entity);
            #region Update Balance

            #endregion

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(TreasuryDTO entity)
        {
            var oldEntity = await _unitOfWork.TreasuryDAL.GetByIdAsync(entity.Id);
            var result = await _unitOfWork.TreasuryDAL.UpdateAsync(_mapper.Map<Treasury>(entity));
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.InComing - oldEntity.InComing;
                    clientVendor.Credit += entity.OutComing - oldEntity.OutComing;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
            }
            else if (oldEntity.ClientVendorId.HasValue)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(oldEntity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    //clientVendor.Debit -= entity.Amount;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            Treasury entity = await _unitOfWork.TreasuryDAL.GetByIdAsync(id);
            entity.IsCancel = true;
            var result = await _unitOfWork.TreasuryDAL.UpdateAsync(entity);
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit -= entity.InComing;
                    clientVendor.Credit -= entity.OutComing;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }

                var accountStatment =  _unitOfWork.AccountStatementDAL.GetAsync(x => x.TreasuryId == entity.Id).Result.SingleOrDefault();
                if (accountStatment != null)
                {
                    accountStatment.IsCancel = true;
                    _unitOfWork.AccountStatementDAL.Update(accountStatment);
                }

            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result > 0;
        }
        #endregion

        #region Helper Methods
        private IQueryable<Treasury> ApplyFilert(IQueryable<Treasury> TreasuryList, TreasurySearchDTO searchCriteriaDTO)
        {
            //Filter

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.DateFrom))
            {
                TreasuryList = TreasuryList.Where(x => x.Date.Date >= DateTime.Parse(searchCriteriaDTO.DateFrom).Date);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.DateTo))
            {
                TreasuryList = TreasuryList.Where(x => x.Date.Date <= DateTime.Parse(searchCriteriaDTO.DateTo).Date);
            }
            //if (searchCriteriaDTO.AccountTypeId.HasValue)
            //{
            //    TreasuryList = TreasuryList.Where(x => x.AccountTypeId == searchCriteriaDTO.AccountTypeId);
            //}

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


        private string GenerateSequenceNumber()
        {
            var lastElement = _unitOfWork.TreasuryDAL.GetAllAsync().Result.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Number) + 1;
            return code.ToString();

        }

        private List<AccountStatement> MapAccountStatement(TreasuryDTO treasuryDTO)
        {
            return new List<AccountStatement>()
            {
                new AccountStatement(){
                Date = DateTime.Now.Date,
                ClientVendorId = treasuryDTO.ClientVendorId.Value,
                BeneficiaryName = treasuryDTO.BeneficiaryName,
                PaymentMethodId = treasuryDTO.PaymentMethodId,
                IsBilled = true,
                Debit = treasuryDTO.InComing,
                Credit = treasuryDTO.OutComing,
                Notes = treasuryDTO.Notes
                }
            };
        }


        #endregion

    }
}
