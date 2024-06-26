﻿using AutoMapper;
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
using Shared.Entities.Setup;
using Data.Entities.Accouting;
using Data.Entities.Setup;
using Shared.Enums;
using System.Diagnostics;
using Data.Contexts;
using Data.Entities.Purchases;

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
            var salesBillHeaderList = await _unitOfWork.SalesBillHeaderDAL.GetAllAsync();

            #region Apply Filters
            salesBillHeaderList.OrderByDescending(x => x.Id);
            salesBillHeaderList = ApplyFilert(salesBillHeaderList, searchCriteriaDTO);
            int total = salesBillHeaderList.Count();
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
            var tt = _mapper.Map<SalesBillHeaderDTO>(await _unitOfWork.SalesBillHeaderDAL.GetByIdAsync(id));
            return tt;
        }

        public async Task<List<ClientVendorBalanceDTO>> GetAllByClientId(long clientId)
        {
            List<ClientVendorBalanceDTO> clientVendorBalanceList = new List<ClientVendorBalanceDTO>();
            var salesBillHeaderList = _unitOfWork.SalesBillHeaderDAL.GetAllByClientId(clientId).Result.ToList();
            if (salesBillHeaderList != null && salesBillHeaderList.Count > 0)
            {
                var clientVendor = salesBillHeaderList[0].ClientVendor;
                //Set initial Oppening Balance
                ClientVendorBalanceDTO clientVendorBalanceDTO = new ClientVendorBalanceDTO();
                clientVendorBalanceDTO.Date = clientVendor.Created.Value.ToString("yyyy-MM-dd");
                if (clientVendor.OppeningBalance > 0)
                    clientVendorBalanceDTO.Debit = clientVendor.OppeningBalance;
                else
                    clientVendorBalanceDTO.Credit = clientVendor.OppeningBalance;
                clientVendorBalanceDTO.Details = "General.OppeningBalance";
                clientVendorBalanceDTO.ClientVendorId = clientId;
                clientVendorBalanceList.Add(clientVendorBalanceDTO);
                foreach (var s in salesBillHeaderList)
                {
                    clientVendorBalanceList.Add(new ClientVendorBalanceDTO()
                    {
                        Date = s.Date.ToString("yyyy-MM-dd"),
                        Debit = s.Paid,
                        Credit = -s.TotalAmount,
                        Details = "General.BillNo",
                        Number = s.Number,
                        RefId = s.Id,
                        ClientVendorId = s.ClientVendorId
                    });
                }
            }
            return clientVendorBalanceList;
        }


        public async Task<ResponseEntityList<SalesBillHeaderDTO>> GetAllLite()
        {
            return new ResponseEntityList<SalesBillHeaderDTO>()
            {
                List = _mapper.Map<IEnumerable<SalesBillHeaderDTO>>(_unitOfWork.SalesBillHeaderDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.SalesBillHeaderDAL.GetAllLiteAsync().Result.Count()
            };
        }

        #endregion

        #region Command
        public async Task<long> Add(SalesBillHeaderDTO entity)
        {
            #region Update Product
            if (entity.IsTemp == false)
            {
                foreach (var item in entity.SalesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                    product.ActualQuantity = entity.IsReturned ? product.ActualQuantity + item.Quantity : product.ActualQuantity - item.Quantity;
                    product.LastSellingPrice = item.PriceAfterDiscount;
                   
                    if (entity.ChangeProductPriceFromSales == true)
                    {
                        product.SellingPrice = item.Price;
                    }
                    await _unitOfWork.ProductDAL.UpdateAsync(product);
                }
            }
            #endregion

            #region Add Sales , AccountStatement and Treasury
            entity.Number = GenerateSequenceNumber();
            var salesBillHeader = _mapper.Map<SalesBillHeader>(entity);
            if (entity.IsTemp == false)
            {
                salesBillHeader.AccountStatement = MapAccountStatement(entity);
                salesBillHeader.Treasury = MapTreasury(entity);
            }
            var result = await _unitOfWork.SalesBillHeaderDAL.AddAsync(salesBillHeader);
            #endregion

            #region Update Balance
            if (entity.IsTemp == false)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    if (entity.IsNewReturned)//Return Sales Bill
                    {
                        clientVendor.Debit += entity.TotalAmount;
                        clientVendor.Credit += entity.Paid;
                    }
                    else//Normal Sales Bill
                    {
                        clientVendor.Debit += entity.Paid;
                        clientVendor.Credit += entity.TotalAmount;
                    }

                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
            }
            #endregion

            _unitOfWork.Complete();

            #region Set BillId to AccountStatment
            if (salesBillHeader.AccountStatementId.HasValue)
            {
                var accountStatement = _unitOfWork.AccountStatementDAL.GetById(salesBillHeader.AccountStatementId.Value);
                accountStatement.BillId = salesBillHeader.Id;
                _unitOfWork.AccountStatementDAL.Update(accountStatement, false);
                _unitOfWork.Complete();
            }
            #endregion
            return salesBillHeader.Id;
        }

        public async Task<long> Update(SalesBillHeaderDTO entity)
        {
            AccountStatement addedAccountStatement = new AccountStatement();
            Treasury addedTreasury = new Treasury();
            var salesHeader = _mapper.Map<SalesBillHeader>(entity);
            var tempSalesBillDetailList = entity.SalesBillDetailList;
            var exsitedSalesBillDetailList = await _unitOfWork.SalesBillDetailDAL.GetAllByHeaderId(entity.Id);

            #region Update SalesBillHeader

            await _unitOfWork.SalesBillDetailDAL.DeleteRange(exsitedSalesBillDetailList.ToList());
            foreach (var item in entity.SalesBillDetailList)
            {
                item.SalesBillHeaderId = entity.Id;
            }
            await _unitOfWork.SalesBillDetailDAL.AddRange(_mapper.Map<List<SalesBillDetail>>(entity.SalesBillDetailList));
            salesHeader.SalesBillDetailList = null;

            var result = await _unitOfWork.SalesBillHeaderDAL.UpdateAsync(salesHeader);
            #endregion

            if (entity.IsTemp == false)
            {


                #region Update Product 

                foreach (var item in tempSalesBillDetailList)
                {
                    var exsitedSalesBillDetails = exsitedSalesBillDetailList.SingleOrDefault(x => x.Id == item.Id && x.ProductId == item.ProductId);
                    decimal quantity = exsitedSalesBillDetails != null && entity.IsTempPageMode==false ? item.Quantity - exsitedSalesBillDetails.Quantity : item.Quantity;
                    var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity + quantity : product.ActualQuantity - quantity;
                    if (entity.ChangeProductPriceFromSales == true)
                    {
                        product.SellingPrice = item.Price;
                    }
                    product.LastSellingPrice = item.PriceAfterDiscount;
                    await _unitOfWork.ProductDAL.UpdateAsync(product);
                }
                #endregion

                #region Update Balance

                var exsitedSalesHeader = await _unitOfWork.SalesBillHeaderDAL.GetByIdAsync(entity.Id);
                exsitedSalesHeader.ClientVendor = null;
                var existedClientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId);
                if (existedClientVendor != null)
                {
                    //if (entity.IsNewReturned == true)
                    //{
                    //    existedClientVendor.Debit -= entity.Paid;
                    //    existedClientVendor.Credit -= entity.TotalAmount;
                    //}

                    //Convert Temp To Sales Bill
                    if (entity.IsTemp == false && exsitedSalesHeader.IsTemp == true)
                    {
                        existedClientVendor.Debit += entity.Paid;
                        existedClientVendor.Credit += entity.TotalAmount;
                    }

                    //Edit Return Bill
                    else if (entity.IsReturned)
                    {
                        existedClientVendor.Debit += entity.TotalAmount - exsitedSalesHeader.TotalAmount;
                        existedClientVendor.Credit += entity.Paid - exsitedSalesHeader.Paid;
                    }


                    else//Normal Sales Bill
                    {
                        existedClientVendor.Debit += entity.Paid - exsitedSalesHeader.Paid;
                        existedClientVendor.Credit += entity.TotalAmount - exsitedSalesHeader.TotalAmount;
                    }

                    await _unitOfWork.ClientVendorDAL.UpdateAsync(existedClientVendor);
                }
                #endregion


                #region Update AccountStatement

                if (entity.AccountStatementId.HasValue)
                {
                    var accountStatement = await _unitOfWork.AccountStatementDAL.GetByIdAsync(entity.AccountStatementId.Value);
                    accountStatement.PaymentMethodId = entity.PaymentMethodId;
                    accountStatement.RefNo = entity.RefNo;
                    accountStatement.Date = DateTime.Parse(entity.Date);
                    accountStatement.RepresentiveId = entity.RepresentiveId;

                    if (entity.IsReturned)
                    {
                        accountStatement.Debit = entity.TotalAmount;
                        accountStatement.Credit = entity.Paid;
                        accountStatement.Notes = "فاتورة مرتجعات المبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
                    }
                    else
                    {
                        accountStatement.Debit = entity.Paid;
                        accountStatement.Credit = entity.TotalAmount;
                        accountStatement.Notes = "فاتورة المبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
                    }
                    await _unitOfWork.AccountStatementDAL.UpdateAsync(accountStatement);
                }
                else
                {
                    #region Add Sales and AccountStatement
                    addedAccountStatement = MapAccountStatement(entity);
                    await _unitOfWork.AccountStatementDAL.AddAsync(addedAccountStatement);
                    #endregion
                }
                #endregion

                #region Update Treasury

                if (entity.TreasuryId.HasValue)
                {
                    var treasury = await _unitOfWork.TreasuryDAL.GetByIdAsync(entity.TreasuryId.Value);
                    treasury.PaymentMethodId = entity.PaymentMethodId;
                    treasury.RefNo = entity.RefNo;
                    treasury.Date = DateTime.Parse(entity.Date);
                    if (entity.IsReturned)
                    {
                        treasury.OutComing = entity.Paid;
                        treasury.Notes = "فاتورة مرتجعات المبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
                    }
                    else
                    {
                        treasury.InComing = entity.Paid;
                        treasury.Notes = "فاتورة مبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
                    }
                    await _unitOfWork.TreasuryDAL.UpdateAsync(treasury);
                }
                else
                {
                    #region Add  Treasury
                    addedTreasury = MapTreasury(entity);
                    await _unitOfWork.TreasuryDAL.AddAsync(addedTreasury);
                    #endregion
                }
                #endregion

            }
            await _unitOfWork.CompleteAsync();

            #region Update TreauryId in SalesHeader
            if (salesHeader.AccountStatementId == null && addedAccountStatement.Id > 0)
            {
                salesHeader.AccountStatementId = addedAccountStatement.Id;
                await _unitOfWork.SalesBillHeaderDAL.UpdateAsync(salesHeader);
                await _unitOfWork.CompleteAsync();
            }

            if (salesHeader.TreasuryId == null && addedTreasury.Id > 0)
            {
                salesHeader.TreasuryId = addedTreasury.Id;
                await _unitOfWork.SalesBillHeaderDAL.UpdateAsync(salesHeader);
                await _unitOfWork.CompleteAsync();
            }
            #endregion

            #region Update Removed Products 
            if (!entity.IsTemp)
            {
                if (entity.RemovedSalesBillDetailList != null && entity.RemovedSalesBillDetailList.Count() > 0)
                {
                    foreach (var item in entity.RemovedSalesBillDetailList)
                    {
                        var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                        product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity - item.Quantity : product.ActualQuantity + item.Quantity;
                        await _unitOfWork.ProductDAL.UpdateAsync(product);
                    }
                }
                await _unitOfWork.CompleteAsync();
            }
            #endregion
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            SalesBillHeader entity = await _unitOfWork.SalesBillHeaderDAL.GetByIdAsync(id);
            entity.ClientVendor = null;//To remove tracking
            if (entity.IsTemp == false)
            {
                #region Update Product
                foreach (var item in entity.SalesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity - item.Quantity : product.ActualQuantity + item.Quantity;
                    product.SellingPrice = item.Price;
                    await _unitOfWork.ProductDAL.UpdateAsync(product);
                }
                #endregion

                #region Update Balance

                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    if (entity.IsReturned == true)
                    {
                        clientVendor.Debit -= entity.TotalAmount;
                        clientVendor.Credit -= entity.Paid;
                    }
                    else
                    {
                        clientVendor.Debit -= entity.Paid;
                        clientVendor.Credit -= entity.TotalAmount;
                    }

                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
                #endregion

                #region Update AccountStatement
                if (entity.AccountStatementId.HasValue)
                {
                    var accountStatement = await _unitOfWork.AccountStatementDAL.GetByIdAsync(entity.AccountStatementId.Value);

                    if (entity.IsReturned)
                    {
                        accountStatement.Debit += entity.TotalAmount;
                        accountStatement.Credit += entity.Paid;
                        //accountStatement.Notes = "فاتورة مرتجعات";
                    }
                    else
                    {
                        accountStatement.Debit -= entity.Paid;
                        accountStatement.Credit -= entity.TotalAmount;
                        //accountStatement.Notes = "فاتورة";
                    }
                    accountStatement.IsCancel = true;
                    await _unitOfWork.AccountStatementDAL.UpdateAsync(accountStatement);
                }
                #endregion


                #region Update Treasury
                if (entity.TreasuryId.HasValue)
                {
                    var treasury = await _unitOfWork.TreasuryDAL.GetByIdAsync(entity.TreasuryId.Value);

                    if (entity.IsReturned)
                    {
                        treasury.OutComing += entity.Paid;
                    }
                    else
                    {
                        treasury.InComing -= entity.Paid;
                    }
                    treasury.IsCancel = true;
                    await _unitOfWork.TreasuryDAL.UpdateAsync(treasury);
                }
                #endregion

            }

            #region Update SalesBillHeader
            entity.IsCancel = true;
            var result = await _unitOfWork.SalesBillHeaderDAL.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            #endregion

            return result > 0;
        }
        #endregion

        #region Helper Methods
        private IQueryable<SalesBillHeader> ApplyFilert(IQueryable<SalesBillHeader> salesBillHeaderList, SalesBillHeaderSearchDTO searchCriteriaDTO)
        {
            salesBillHeaderList = salesBillHeaderList.Where(x => x.IsCancel == false);
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Number))
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.Number.Contains(searchCriteriaDTO.Number));
            }

            if (searchCriteriaDTO.ClientVendorId.HasValue)
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.ClientVendorId == searchCriteriaDTO.ClientVendorId);
            }

            if (searchCriteriaDTO.RepresentiveId.HasValue)
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.RepresentiveId == searchCriteriaDTO.RepresentiveId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.DateFrom))
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.Date.Date >= DateTime.Parse(searchCriteriaDTO.DateFrom).Date);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.DateTo))
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.Date.Date <= DateTime.Parse(searchCriteriaDTO.DateTo).Date);
            }


            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.PersonPhoneNumber))
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.ClientVendor.PhoneNumber1 == searchCriteriaDTO.PersonPhoneNumber || x.ClientVendor.PhoneNumber2 == searchCriteriaDTO.PersonPhoneNumber);
            }

            if (searchCriteriaDTO.IsTemp.HasValue)
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.IsTemp == searchCriteriaDTO.IsTemp.Value);
            }

            if (searchCriteriaDTO.IsReturned.HasValue)
            {
                salesBillHeaderList = salesBillHeaderList.Where(x => x.IsReturned == searchCriteriaDTO.IsReturned.Value);
            }
            return salesBillHeaderList;
        }


        private AccountStatement MapAccountStatement(SalesBillHeaderDTO entity)
        {
            AccountStatement accountStatement = new AccountStatement()
            {
                Date = DateTime.Parse(entity.Date),
                ClientVendorId = entity.ClientVendorId,
                BeneficiaryName = entity.ClientVendorName,
                //TransactionTypeId = TransactionTypeEnum.Incoming,
                PaymentMethodId = entity.PaymentMethodId,
                RefNo = entity.RefNo,
                IsBilled = true,
                BillId = entity.Id,
                RepresentiveId = entity.RepresentiveId,
                BillType = BillTypeEnum.Sales
            };

            if (entity.IsReturned)
            {
                accountStatement.Debit = entity.TotalAmount;
                accountStatement.Credit = entity.Paid;
                accountStatement.Notes = "فاتورة مرتجعات المبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
            }
            else
            {
                accountStatement.Debit = entity.Paid;
                accountStatement.Credit = entity.TotalAmount;
                accountStatement.Notes = "فاتورة مبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
            }

            return accountStatement;
        }

        private Treasury MapTreasury(SalesBillHeaderDTO entity)
        {
            Treasury accountStatement = new Treasury()
            {

                Date = DateTime.Parse(entity.Date),
                AccountTypeId = AccountTypeEnum.Clients,
                ClientVendorId = entity.ClientVendorId,
                BeneficiaryName = entity.ClientVendorName,
                PaymentMethodId = entity.PaymentMethodId,
                RefNo = entity.RefNo,
                IsBilled = true,
                Number = GenerateTreasurySequenceNumber(),

            };

            if (entity.IsReturned)
            {
                accountStatement.InComing = 0;
                accountStatement.OutComing = entity.Paid;
                accountStatement.Notes = "فاتورة مرتجعات المبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;

            }
            else
            {
                accountStatement.InComing = entity.Paid;
                accountStatement.OutComing = 0;
                accountStatement.Notes = "فاتورة مبيعات" + "(" + entity.Number + ")" + " " + entity.Notes;
            }

            return accountStatement;
        }

        private string GenerateTreasurySequenceNumber()
        {
            var lastElement = _unitOfWork.TreasuryDAL.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Number) + 1;
            return code.ToString();

        }

        private string GenerateSequenceNumber()
        {
            lock (this)
            {

                var lastElement = _unitOfWork.SalesBillHeaderDAL.GetAllAsync().Result.OrderByDescending(x => x.Id).FirstOrDefault();

                if (lastElement == null)
                {
                    return "1000";
                }
                int code = int.Parse(lastElement.Number) + 1;
                return code.ToString();
            }

        }


        #endregion
    }
}
