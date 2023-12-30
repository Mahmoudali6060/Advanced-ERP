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
using Shared.Entities.Setup;
using Data.Entities.Accouting;
using Data.Entities.Setup;
using Shared.Enums;
using System.Diagnostics;

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

            #region Apply Filters
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
            var tt = _mapper.Map<SalesBillHeaderDTO>(await _unitOfWork.SalesBillHeaderDAL.GetById(id));
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
                clientVendorBalanceDTO.Date = clientVendor.Created.ToString("yyyy-MM-dd");
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
                        Credit = -s.TotalAfterDiscount,
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
                List = _mapper.Map<IEnumerable<SalesBillHeaderDTO>>(_unitOfWork.SalesBillHeaderDAL.GetAllLite().Result),
                Total = _unitOfWork.SalesBillHeaderDAL.GetAllLite().Result.Count()
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
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = entity.IsReturned ? product.ActualQuantity + item.Quantity : product.ActualQuantity - item.Quantity;
                    product.Price = item.Price;
                    await _unitOfWork.ProductDAL.Update(product);
                }
            }
            #endregion

            #region Add Sales and Treasury
            var salesBillHeader = _mapper.Map<SalesBillHeader>(entity);
            salesBillHeader.Treasury = MapTreasury(entity);
            var result = await _unitOfWork.SalesBillHeaderDAL.Add(salesBillHeader);
            #endregion

            #region Update Balance
            if (entity.IsTemp == false)
            {
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    if (entity.IsReturned)
                    {
                        clientVendor.Debit += entity.TotalAfterDiscount;
                        clientVendor.Credit += entity.Paid;
                    }
                    else
                    {
                        clientVendor.Debit += entity.Paid;
                        clientVendor.Credit += entity.TotalAfterDiscount;
                    }

                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            #endregion

            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<long> Update(SalesBillHeaderDTO entity)
        {
            var salesHeader = _mapper.Map<SalesBillHeader>(entity);
            var tempSalesBillDetailList = entity.SalesBillDetailList;
            var exsitedSalesBillDetailList = await _unitOfWork.SalesBillDetailDAL.GetAllByHeaderId(entity.Id);

            if (entity.IsTemp == false)
            {
                #region Update Product 

                foreach (var item in tempSalesBillDetailList)
                {
                    var exsitedSalesBillDetails = exsitedSalesBillDetailList.SingleOrDefault(x => x.Id == item.Id && x.ProductId == item.ProductId);
                    decimal quantity = exsitedSalesBillDetails != null ? exsitedSalesBillDetails.Quantity : item.Quantity;
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity + quantity : product.ActualQuantity - quantity;
                    product.Price = item.Price;
                    await _unitOfWork.ProductDAL.Update(product);
                }
                #endregion

                #region Update Balance

                var exsitedSalesHeader = await _unitOfWork.SalesBillHeaderDAL.GetById(entity.Id);
                exsitedSalesHeader.ClientVendor = null;
                var existedClientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (existedClientVendor != null)
                {
                    if (entity.IsReturned == true)
                    {
                        existedClientVendor.Debit -= entity.Paid - exsitedSalesHeader.Paid;
                        existedClientVendor.Credit -= entity.TotalAfterDiscount - exsitedSalesHeader.TotalAfterDiscount;
                    }
                    else
                    {
                        existedClientVendor.Debit += entity.Paid - exsitedSalesHeader.Paid;
                        existedClientVendor.Credit += entity.TotalAfterDiscount - exsitedSalesHeader.TotalAfterDiscount;
                    }

                    await _unitOfWork.ClientVendorDAL.Update(existedClientVendor);
                }
                #endregion

                #region Update Treasury

                if (entity.TreasuryId.HasValue)
                {
                    var treasury = await _unitOfWork.TreasuryDAL.GetById(entity.TreasuryId.Value);

                    if (entity.IsReturned == true)
                    {
                        treasury.Debit -= entity.Paid;
                        treasury.Credit -= entity.TotalAfterDiscount;
                    }
                    else
                    {
                        treasury.Debit = entity.Paid;
                        treasury.Credit = entity.TotalAfterDiscount;
                    }
                    await _unitOfWork.TreasuryDAL.Update(treasury);
                }
                else
                {
                    #region Add Sales and Treasury
                    salesHeader.Treasury = MapTreasury(entity);
                    #endregion
                }
                #endregion

            }
            #region Update SalesBillHeader

            await _unitOfWork.SalesBillDetailDAL.DeleteRange(exsitedSalesBillDetailList.ToList());
            foreach (var item in entity.SalesBillDetailList)
            {
                item.SalesBillHeaderId = entity.Id;
            }
            await _unitOfWork.SalesBillDetailDAL.AddRange(_mapper.Map<List<SalesBillDetail>>(entity.SalesBillDetailList));
            salesHeader.SalesBillDetailList = null;
            var result = await _unitOfWork.SalesBillHeaderDAL.Update(salesHeader);
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            SalesBillHeader entity = await _unitOfWork.SalesBillHeaderDAL.GetById(id);
            entity.ClientVendor = null;//To remove tracking
            if (entity.IsTemp == false)
            {
                #region Update Product
                foreach (var item in entity.SalesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity - item.Quantity : product.ActualQuantity + item.Quantity;
                    product.Price = item.Price;
                    await _unitOfWork.ProductDAL.Update(product);
                }
                #endregion

                #region Update Balance

                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    if (entity.IsReturned == true)
                    {
                        clientVendor.Debit += entity.TotalAfterDiscount;
                        clientVendor.Credit += entity.Paid;
                    }
                    else
                    {
                        clientVendor.Debit -= entity.TotalAfterDiscount;
                        clientVendor.Credit -= entity.Paid;
                    }

                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
                #endregion

                #region Update Treasury
                var treasury = await _unitOfWork.TreasuryDAL.GetById(entity.TreasuryId.Value);
                //if (entity.IsReturned == true)
                //{
                //    treasury.Debit -= entity.Paid;
                //    treasury.Credit -= entity.TotalAfterDiscount;
                //}
                //else
                //{
                //    treasury.Debit += entity.Paid;
                //    treasury.Credit += entity.TotalAfterDiscount;
                //}
                treasury.IsCancel = true;
                await _unitOfWork.TreasuryDAL.Update(treasury);
                #endregion
            }

            #region Update SalesBillHeader
            entity.IsCancel = true;
            var result = await _unitOfWork.SalesBillHeaderDAL.Update(entity);
            await _unitOfWork.CompleteAsync();
            #endregion

            return result > 0;
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


        private Treasury MapTreasury(SalesBillHeaderDTO entity)
        {
            Treasury treasury = new Treasury()
            {
                Date = DateTime.Parse(entity.Date),
                AccountTypeId = AccountTypeEnum.Clients,
                ClientVendorId = entity.ClientVendorId,
                BeneficiaryName = entity.ClientVendorName,
                //TransactionTypeId = TransactionTypeEnum.Incoming,
                PaymentMethodId = PaymentMethodEnum.Cash,
                RefNo = entity.Number,
            };

            if (entity.IsReturned)
            {
                treasury.Debit = entity.TotalAfterDiscount;
                treasury.Credit = entity.Paid;
                treasury.Notes = "فاتورة مرتجعات";

            }
            else
            {
                treasury.Debit = entity.Paid;
                treasury.Credit = entity.TotalAfterDiscount;
                treasury.Notes = "فاتورة";

            }

            return treasury;
        }

        #endregion
    }
}
