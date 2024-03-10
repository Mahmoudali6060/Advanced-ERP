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
using Data.Contexts;

namespace DataService.Accounting.Handlers
{
    public class AccountStatementDSL : IAccountStatementDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public AccountStatementDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<AccountStatementDTO>> GetAll(AccountStatementSearchDTO searchCriteriaDTO)
        {
            var treasuryList = await _unitOfWork.AccountStatementDAL.GetAsync(x => x.IsCancel == false, x => x.ClientVendor);
            #region Apply Filters
            treasuryList = treasuryList.OrderByDescending(x => x.Id);
            treasuryList = ApplyFilert(treasuryList, searchCriteriaDTO);
            int total = treasuryList.Count();

            #endregion

            #region Apply Pagination
            treasuryList = treasuryList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var treasuryDTOList = _mapper.Map<IEnumerable<AccountStatementDTO>>(treasuryList);
            return new ResponseEntityList<AccountStatementDTO>
            {
                List = treasuryDTOList,
                Total = total,
            };
            #endregion

        }

        public async Task<AccountStatementGridDTO> GetAllForGrid(AccountStatementSearchDTO searchCriteriaDTO)
        {
            var treasuryList = await _unitOfWork.AccountStatementDAL.GetAsync(x => x.IsCancel == false, x => x.ClientVendor);
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
            var treasuryDTOList = _mapper.Map<IEnumerable<AccountStatementDTO>>(treasuryList);
            return new AccountStatementGridDTO
            {
                List = treasuryDTOList,
                Total = total,
                Balance = balance
            };
            #endregion

        }

        public async Task<AccountStatementDTO> GetById(long id)
        {
            return _mapper.Map<AccountStatementDTO>(await _unitOfWork.AccountStatementDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<AccountStatementDTO>> GetAllLite()
        {
            return new ResponseEntityList<AccountStatementDTO>()
            {
                List = _mapper.Map<IEnumerable<AccountStatementDTO>>(_unitOfWork.AccountStatementDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.AccountStatementDAL.GetAllLiteAsync().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(AccountStatementDTO entityDTO)
        {
            entityDTO.Number = GenerateSequenceNumber();
            var entity = _mapper.Map<AccountStatement>(entityDTO);
            await _unitOfWork.AccountStatementDAL.AddAsync(entity);
            #region Update Balance
            if (entityDTO.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entityDTO.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.Debit;
                    clientVendor.Credit += entity.Credit;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
            }
            #endregion

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(AccountStatementDTO entity)
        {
            var oldEntity = await _unitOfWork.AccountStatementDAL.GetByIdAsync(entity.Id);
            var result = await _unitOfWork.AccountStatementDAL.UpdateAsync(_mapper.Map<AccountStatement>(entity));
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.Debit - oldEntity.Debit;
                    clientVendor.Credit += entity.Credit - oldEntity.Credit;
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
            AccountStatement entity = await _unitOfWork.AccountStatementDAL.GetByIdAsync(id);
            var result = await _unitOfWork.AccountStatementDAL.DeleteAsync(entity);
            #region Update Balance
            if (entity.ClientVendorId.HasValue == true)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId.Value);
                if (clientVendor != null)
                {
                    clientVendor.Debit -= entity.Debit;
                    clientVendor.Credit -= entity.Credit;
                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<AccountStatement> ApplyFilert(IQueryable<AccountStatement> AccountStatementList, AccountStatementSearchDTO searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Date))
            {
                AccountStatementList = AccountStatementList.Where(x => x.Date.Date == DateTime.Parse(searchCriteriaDTO.Date));
            }


            if (searchCriteriaDTO.ClientVendorId.HasValue)
            {
                AccountStatementList = AccountStatementList.Where(x => x.ClientVendorId == searchCriteriaDTO.ClientVendorId);
            }
            if (searchCriteriaDTO.RepresentiveId.HasValue)
            {
                AccountStatementList = AccountStatementList.Where(x => x.RepresentiveId == searchCriteriaDTO.RepresentiveId);
            }
            if (searchCriteriaDTO.PaymentMethodId.HasValue)
            {
                AccountStatementList = AccountStatementList.Where(x => x.PaymentMethodId == searchCriteriaDTO.PaymentMethodId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.RefNo))
            {
                AccountStatementList = AccountStatementList.Where(x => x.RefNo == searchCriteriaDTO.RefNo);
            }

            return AccountStatementList;
        }


        private string GenerateSequenceNumber()
        {
            var lastElement = _unitOfWork.AccountStatementDAL.GetAllAsync().Result.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Number) + 1;
            return code.ToString();

        }

        #endregion

    }
}
