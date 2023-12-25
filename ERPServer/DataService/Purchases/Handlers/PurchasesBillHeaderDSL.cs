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
using Shared.Entities.Setup;
using Data.Entities.Accouting;
using Shared.Entities.Sales;
using Shared.Enums;
using Data.Entities.Sales;

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

        public async Task<PurchasesBillHeaderDTO> GetByNumber(string number)
        {
            return _mapper.Map<PurchasesBillHeaderDTO>(await _unitOfWork.PurchasesBillHeaderDAL.GetByNumber(number));
        }

        public async Task<ResponseEntityList<PurchasesBillHeaderDTO>> GetAllLite()
        {
            return new ResponseEntityList<PurchasesBillHeaderDTO>()
            {
                List = _mapper.Map<IEnumerable<PurchasesBillHeaderDTO>>(_unitOfWork.PurchasesBillHeaderDAL.GetAllLite().Result),
                Total = _unitOfWork.PurchasesBillHeaderDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<List<ClientVendorBalanceDTO>> GetAllByVendorId(long vendorId)
        {
            List<ClientVendorBalanceDTO> clientVendorBalanceList = new List<ClientVendorBalanceDTO>();
            var purchasesBillHeaderList = _unitOfWork.PurchasesBillHeaderDAL.GetAllByVendorId(vendorId).Result.ToList();
            if (purchasesBillHeaderList != null && purchasesBillHeaderList.Count > 0)
            {
                var clientVendor = purchasesBillHeaderList[0].ClientVendor;
                //Set initial Oppening Balance
                ClientVendorBalanceDTO clientVendorBalanceDTO = new ClientVendorBalanceDTO();
                clientVendorBalanceDTO.Date = clientVendor.Created.ToString("yyyy-MM-dd");
                if (clientVendor.OppeningBalance > 0)
                    clientVendorBalanceDTO.Debit = clientVendor.OppeningBalance;
                else
                    clientVendorBalanceDTO.Credit = clientVendor.OppeningBalance;
                clientVendorBalanceDTO.Details = "General.OppeningBalance";
                clientVendorBalanceDTO.ClientVendorId = vendorId;
                clientVendorBalanceList.Add(clientVendorBalanceDTO);
                foreach (var s in purchasesBillHeaderList)
                {
                    clientVendorBalanceList.Add(new ClientVendorBalanceDTO()
                    {
                        Date = s.Date.ToString("yyyy-MM-dd"),
                        Debit = s.TotalAfterDiscount,
                        Credit = -s.Paid,
                        Details = "General.BillNo",
                        Number = s.Number,
                        RefId = s.Id,
                        ClientVendorId = s.ClientVendorId
                    });
                }
            }
            return clientVendorBalanceList;
        }

        #endregion

        #region Command
        public async Task<long> Add(PurchasesBillHeaderDTO entity)
        {
            #region Add PurchasesBillHeader
            var purchasesBillHeader = _mapper.Map<PurchasesBillHeader>(entity);
            purchasesBillHeader.Treasury = MapTreasury(entity);
            var result = await _unitOfWork.PurchasesBillHeaderDAL.Add(purchasesBillHeader);
            #endregion

            if (entity.IsTemp == false)
            {
                #region Update Product

                foreach (var item in entity.PurchasesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = product.ActualQuantity + item.Quantity;
                    product.LastPurchasingPrice = item.PriceAfterDiscount;
                    await _unitOfWork.ProductDAL.Update(product);
                }
                #endregion

                #region Update Balance

                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    clientVendor.Debit += entity.TotalAfterDiscount;
                    clientVendor.Credit += entity.Paid;

                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
                #endregion
            }

            await _unitOfWork.CompleteAsync();
            return result;
        }


        public async Task<long> Update(PurchasesBillHeaderDTO entity)
        {
            #region Update PurchasesBillHeader
            var exsitedPurhaseDetailsList = await _unitOfWork.PurchasesBillDetailDAL.GetAllByHeaderId(entity.Id);

            await _unitOfWork.PurchasesBillDetailDAL.DeleteRange(exsitedPurhaseDetailsList.ToList());
            foreach (var item in entity.PurchasesBillDetailList)
            {
                item.PurchasesBillHeaderId = entity.Id;
            }
            await _unitOfWork.PurchasesBillDetailDAL.AddRange(_mapper.Map<List<PurchasesBillDetail>>(entity.PurchasesBillDetailList));

            var tempPurchasesBillDetailList = entity.PurchasesBillDetailList;
            entity.PurchasesBillDetailList = null;

            var result = await _unitOfWork.PurchasesBillHeaderDAL.Update(_mapper.Map<PurchasesBillHeader>(entity));
            #endregion

            if (entity.IsTemp == false)
            {
                #region Update Product 

                foreach (var item in tempPurchasesBillDetailList)
                {
                    var exsitedPurchaseDetails = exsitedPurhaseDetailsList.SingleOrDefault(x => x.Id == item.Id);
                    decimal quantity = exsitedPurchaseDetails != null ? exsitedPurchaseDetails.Quantity : item.Quantity;
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = product.ActualQuantity + quantity;
                    product.LastPurchasingPrice = item.PriceAfterDiscount;
                    await _unitOfWork.ProductDAL.Update(product);
                }
                #endregion

                #region Update Balance

                var exsitedPurhaseHeader = await _unitOfWork.PurchasesBillHeaderDAL.GetById(entity.Id);
                exsitedPurhaseHeader.ClientVendor = null;
                var existedClientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (existedClientVendor != null)
                {
                    existedClientVendor.Debit += entity.TotalAfterDiscount - exsitedPurhaseHeader.TotalAfterDiscount;
                    existedClientVendor.Credit += entity.Paid - exsitedPurhaseHeader.Paid;
                    await _unitOfWork.ClientVendorDAL.Update(existedClientVendor);
                }
                #endregion

                #region Update Treasury
                var treasury = await _unitOfWork.TreasuryDAL.GetById(entity.TreasuryId.Value);
                treasury.Credit = entity.TotalAfterDiscount;
                treasury.Debit = entity.Paid;
                await _unitOfWork.TreasuryDAL.Update(treasury);
                #endregion
            }
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            PurchasesBillHeader entity = await _unitOfWork.PurchasesBillHeaderDAL.GetById(id);
            entity.ClientVendor = null;//To remove tracking

            if (entity.IsTemp == false)
            {
                #region Update Product
                foreach (var item in entity.PurchasesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetById(item.ProductId);
                    product.ActualQuantity = product.ActualQuantity - item.Quantity;
                    product.Price = item.Price;
                    await _unitOfWork.ProductDAL.Update(product);
                }
                #endregion

                #region Update Balance
                var clientVendor = await _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    clientVendor.Debit -= entity.TotalAfterDiscount;
                    clientVendor.Credit -= entity.Paid;
                    await _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
                #endregion

                #region Update Treasury
                var treasury = await _unitOfWork.TreasuryDAL.GetById(entity.TreasuryId.Value);
                treasury.IsCancel = true;
                await _unitOfWork.TreasuryDAL.Update(treasury);
                #endregion

            }
            entity.IsCancel = true;
            var result = await _unitOfWork.PurchasesBillHeaderDAL.Update(entity);
            await _unitOfWork.CompleteAsync();
            return result > 0;
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
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.ClientVendorId == searchCriteriaDTO.VendorId);
            }

            if (searchCriteriaDTO.IsTemp.HasValue)
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.IsTemp == searchCriteriaDTO.IsTemp.Value);
            }

            if (searchCriteriaDTO.IsReturned.HasValue)
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.IsReturned == searchCriteriaDTO.IsReturned.Value);
            }
            return purchasesBillHeaderList;
        }


        private Treasury MapTreasury(PurchasesBillHeaderDTO entity)
        {
            return new Treasury()
            {
                Date = DateTime.Parse(entity.Date),
                AccountTypeId = AccountTypeEnum.Vendors,
                ClientVendorId = entity.ClientVendorId,
                BeneficiaryName = entity.ClientVendorName,
                //TransactionTypeId = TransactionTypeEnum.Incoming,
                PaymentMethodId = PaymentMethodEnum.Cash,
                Debit = entity.Paid,
                Credit = entity.TotalAfterDiscount,
                RefNo = entity.Number,
                Notes = entity.Notes
            };
        }

        #endregion
    }
}
