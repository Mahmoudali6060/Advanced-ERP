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
            foreach (var item in entity.SalesBillDetailList)
            {
                var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                product.ActualQuantity = product.ActualQuantity - item.Quantity;
                product.Price = item.Price;
                await _unitOfWork.ProductDAL.Update(product);
            }
            #endregion

            var result = await _unitOfWork.SalesBillHeaderDAL.Add(_mapper.Map<SalesBillHeader>(entity));

            #region Update Balance
            var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
            if (clientVendor != null)
            {
                clientVendor.Debit += entity.Paid;
                clientVendor.Credit += entity.TotalAfterDiscount;

                await _unitOfWork.ClientVendorDAL.Update(clientVendor);
            }
            #endregion

            await _unitOfWork.CompleteAsync();
            return result;
        }


        public async Task<long> Update(SalesBillHeaderDTO entity)
        {
            var exsitedSalesBillDetailList = await _unitOfWork.SalesBillDetailDAL.GetAllByHeaderId(entity.Id);

            await _unitOfWork.SalesBillDetailDAL.DeleteRange(exsitedSalesBillDetailList.ToList());
            foreach (var item in entity.SalesBillDetailList)
            {
                item.SalesBillHeaderId = entity.Id;
            }
            await _unitOfWork.SalesBillDetailDAL.AddRange(_mapper.Map<List<SalesBillDetail>>(entity.SalesBillDetailList));
            var tempSalesBillDetailList = entity.SalesBillDetailList;
            entity.SalesBillDetailList = null;
            var result = await _unitOfWork.SalesBillHeaderDAL.Update(_mapper.Map<SalesBillHeader>(entity));

            #region Update Product 
            foreach (var item in tempSalesBillDetailList)
            {
                var exsitedSalesBillDetails = exsitedSalesBillDetailList.SingleOrDefault(x => x.Id == item.Id);
                decimal quantity = exsitedSalesBillDetails != null ? item.Quantity - exsitedSalesBillDetails.Quantity : item.Quantity;
                var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                product.ActualQuantity = product.ActualQuantity - quantity;
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
                existedClientVendor.Debit += entity.Paid - exsitedSalesHeader.Paid;
                existedClientVendor.Credit += entity.TotalAfterDiscount - exsitedSalesHeader.TotalAfterDiscount;
                await _unitOfWork.ClientVendorDAL.Update(existedClientVendor);
            }
            #endregion
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            SalesBillHeader entity = await _unitOfWork.SalesBillHeaderDAL.GetById(id);
            entity.ClientVendor = null;//To remove tracking

            #region Update Product
            foreach (var item in entity.SalesBillDetailList)
            {
                var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                product.ActualQuantity = product.ActualQuantity + item.Quantity;
                product.Price = item.Price;
                await _unitOfWork.ProductDAL.Update(product);
            }
            #endregion

            var result = await _unitOfWork.SalesBillHeaderDAL.Delete(entity);

            #region Update Balance
            var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
            if (clientVendor != null)
            {
                clientVendor.Debit -= entity.TotalAfterDiscount;
                clientVendor.Credit -= entity.Paid;
                await _unitOfWork.ClientVendorDAL.Update(clientVendor);
            }
            #endregion

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
