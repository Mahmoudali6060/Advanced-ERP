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
using Shared.Entities.Setup;
using Data.Entities.Accouting;
using Shared.Enums;

namespace DataService.Setup.Handlers
{
    public class PurchasesBillHeaderDSL : IPurchasesBillHeaderDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        private readonly object purchasesBillHeaderLock = new object();
        public PurchasesBillHeaderDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<PurchasesBillHeaderDTO>> GetAll(PurchasesBillHeaderSearchDTO searchCriteriaDTO)
        {
            var purchasesBillHeaderList = await _unitOfWork.PurchasesBillHeaderDAL.GetAllWithIncludes(null, x => x.ClientVendor,x => x.CreatedByProfile,x => x.ModifiedByProfile);

            #region Apply Filters
            purchasesBillHeaderList.OrderByDescending(x => x.Id);
            purchasesBillHeaderList = ApplyFilert(purchasesBillHeaderList, searchCriteriaDTO);
            int total = purchasesBillHeaderList.Count();
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
            var result = await _unitOfWork.PurchasesBillHeaderDAL.GetAllWithIncludes(x => x.Id == id, x => x.ClientVendor, x => x.PurchasesBillDetailList, x => x.Treasury);
            var tt = _mapper.Map<PurchasesBillHeaderDTO>(result.ToList()[0]);
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
                List = _mapper.Map<IEnumerable<PurchasesBillHeaderDTO>>(_unitOfWork.PurchasesBillHeaderDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.PurchasesBillHeaderDAL.GetAllLiteAsync().Result.Count()
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
        public long AddNormal(PurchasesBillHeaderDTO entity)
        {

            #region Update Product
            if (entity.IsTemp == false)
            {
                foreach (var item in entity.PurchasesBillDetailList)
                {

                    var product = _unitOfWork.ProductDAL.GetById(item.ProductId);

                    product.ActualQuantity = entity.IsReturned ? product.ActualQuantity - item.Quantity : product.ActualQuantity + item.Quantity;
                    product.LastPurchasingPrice = item.PriceAfterDiscount;
                    product.Price = product.Price;
                    _unitOfWork.ProductDAL.Update(product);
                }
            }
            #endregion

            #region Add Purchases and Treasury
            entity.Number = GenerateSequenceNumber();
            var purchasesBillHeader = _mapper.Map<PurchasesBillHeader>(entity);
            if (entity.IsTemp == false)
            {
                purchasesBillHeader.Treasury = MapTreasury(entity);
            }
            var result = _unitOfWork.PurchasesBillHeaderDAL.Add(purchasesBillHeader);
            #endregion

            #region Update Balance
            if (entity.IsTemp == false)
            {
                var clientVendor = _unitOfWork.ClientVendorDAL.GetById(entity.ClientVendorId);

                if (clientVendor != null)
                {
                    if (entity.IsNewReturned)//Return Purchases Bill
                    {
                        clientVendor.Debit += entity.Paid;
                        clientVendor.Credit += entity.TotalAfterDiscount;
                    }
                    else//Normal Purchases Bill
                    {
                        clientVendor.Debit += entity.TotalAfterDiscount;
                        clientVendor.Credit += entity.Paid;
                    }

                    _unitOfWork.ClientVendorDAL.Update(clientVendor);
                }
            }
            #endregion

            _unitOfWork.Complete();

            return purchasesBillHeader.Id;
        }

        public async Task<long> Update(PurchasesBillHeaderDTO entity)
        {
            Treasury addedTreasury = new Treasury();
            var purchasesHeader = _mapper.Map<PurchasesBillHeader>(entity);
            var tempPurchasesBillDetailList = entity.PurchasesBillDetailList;
            var exsitedPurchasesBillDetailList = await _unitOfWork.PurchasesBillDetailDAL.GetAllByHeaderId(entity.Id);

            #region Update PurchasesBillHeader

            await _unitOfWork.PurchasesBillDetailDAL.DeleteRange(exsitedPurchasesBillDetailList.ToList());
            foreach (var item in entity.PurchasesBillDetailList)
            {
                item.PurchasesBillHeaderId = entity.Id;
            }
            await _unitOfWork.PurchasesBillDetailDAL.AddRange(_mapper.Map<List<PurchasesBillDetail>>(entity.PurchasesBillDetailList));
            purchasesHeader.PurchasesBillDetailList = null;
            var result = await _unitOfWork.PurchasesBillHeaderDAL.UpdateAsync(purchasesHeader);
            #endregion

            if (entity.IsTemp == false)
            {
                #region Update Product 

                foreach (var item in tempPurchasesBillDetailList)
                {
                    var exsitedPurchasesBillDetails = exsitedPurchasesBillDetailList.SingleOrDefault(x => x.Id == item.Id && x.ProductId == item.ProductId);
                    decimal quantity = exsitedPurchasesBillDetails != null ? exsitedPurchasesBillDetails.Quantity : item.Quantity;
                    var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity - quantity : product.ActualQuantity + quantity;
                    product.Price = item.Price;
                    product.LastPurchasingPrice = item.PriceAfterDiscount;
                    await _unitOfWork.ProductDAL.UpdateAsync(product);
                }
                #endregion

                #region Update Balance

                var exsitedPurchasesHeader = await _unitOfWork.PurchasesBillHeaderDAL.GetByIdAsync(entity.Id);
                exsitedPurchasesHeader.ClientVendor = null;
                var existedClientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId);
                if (existedClientVendor != null)
                {

                    //Convert Temp To Purchases Bill
                    if (entity.IsTemp == false && exsitedPurchasesHeader.IsTemp == true)
                    {
                        existedClientVendor.Debit += entity.TotalAfterDiscount;
                        existedClientVendor.Credit += entity.Paid;
                    }

                    //Edit Return Bill
                    else if (entity.IsReturned)
                    {
                        existedClientVendor.Debit += entity.Paid - exsitedPurchasesHeader.Paid;
                        existedClientVendor.Credit += entity.TotalAfterDiscount - exsitedPurchasesHeader.TotalAfterDiscount;
                    }


                    else//Eidt Normal Purchases Bill
                    {
                        existedClientVendor.Debit += entity.TotalAfterDiscount - exsitedPurchasesHeader.TotalAfterDiscount;
                        existedClientVendor.Credit += entity.Paid - exsitedPurchasesHeader.Paid;
                    }

                    await _unitOfWork.ClientVendorDAL.UpdateAsync(existedClientVendor);
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
                        treasury.Debit = entity.Paid;
                        treasury.Credit = entity.TotalAfterDiscount;
                    }
                    else
                    {
                        treasury.Debit = entity.TotalAfterDiscount;
                        treasury.Credit = entity.Paid;
                    }
                    await _unitOfWork.TreasuryDAL.UpdateAsync(treasury);
                }
                else
                {
                    #region Add Purchases and Treasury
                    addedTreasury = MapTreasury(entity);
                    await _unitOfWork.TreasuryDAL.AddAsync(addedTreasury);
                    #endregion
                }
                #endregion

            }
            await _unitOfWork.CompleteAsync();

            #region Update TreauryId in PurchasesHeader
            if (purchasesHeader.TreasuryId == null && addedTreasury.Id > 0)
            {
                purchasesHeader.TreasuryId = addedTreasury.Id;
                await _unitOfWork.PurchasesBillHeaderDAL.UpdateAsync(purchasesHeader);
                await _unitOfWork.CompleteAsync();
            }
            #endregion

            return result;
        }

        public async Task<bool> Delete(long id)
        {
            PurchasesBillHeader entity = await _unitOfWork.PurchasesBillHeaderDAL.GetByIdAsync(id);
            entity.ClientVendor = null;//To remove tracking
            if (entity.IsTemp == false)
            {
                #region Update Product
                foreach (var item in entity.PurchasesBillDetailList)
                {
                    var product = await _unitOfWork.ProductDAL.GetByIdAsync(item.ProductId);
                    product.ActualQuantity = entity.IsReturned == true ? product.ActualQuantity + item.Quantity : product.ActualQuantity - item.Quantity;
                    product.Price = item.Price;
                    await _unitOfWork.ProductDAL.UpdateAsync(product);
                }
                #endregion

                #region Update Balance

                var clientVendor = await _unitOfWork.ClientVendorDAL.GetByIdAsync(entity.ClientVendorId);
                if (clientVendor != null)
                {
                    if (entity.IsReturned == true)
                    {
                        clientVendor.Debit -= entity.Paid;
                        clientVendor.Credit -= entity.TotalAfterDiscount;
                    }
                    else
                    {
                        clientVendor.Debit -= entity.TotalAfterDiscount;
                        clientVendor.Credit -= entity.Paid;
                    }

                    await _unitOfWork.ClientVendorDAL.UpdateAsync(clientVendor);
                }
                #endregion

                #region Update Treasury
                if (entity.TreasuryId.HasValue)
                {
                    var treasury = await _unitOfWork.TreasuryDAL.GetByIdAsync(entity.TreasuryId.Value);

                    if (entity.IsReturned)
                    {
                        treasury.Debit += entity.Paid;
                        treasury.Credit += entity.TotalAfterDiscount;
                        //treasury.Notes = "فاتورة مرتجعات";
                    }
                    else
                    {
                        treasury.Debit -= entity.TotalAfterDiscount;
                        treasury.Credit -= entity.Paid;
                        //treasury.Notes = "فاتورة";
                    }
                    treasury.IsCancel = true;
                    await _unitOfWork.TreasuryDAL.UpdateAsync(treasury);
                }
                #endregion
            }

            #region Update PurchasesBillHeader
            entity.IsCancel = true;
            var result = await _unitOfWork.PurchasesBillHeaderDAL.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            #endregion

            return result > 0;
        }
        #endregion

        #region Helper Methods
        private IQueryable<PurchasesBillHeader> ApplyFilert(IQueryable<PurchasesBillHeader> purchasesBillHeaderList, PurchasesBillHeaderSearchDTO searchCriteriaDTO)
        {
            purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.IsCancel == false);

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Number))
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.Number.Contains(searchCriteriaDTO.Number));
            }

            if (searchCriteriaDTO.VendorId.HasValue)
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.ClientVendorId == searchCriteriaDTO.VendorId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Date))
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.Date.Date == DateTime.Parse(searchCriteriaDTO.Date).Date);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.PersonPhoneNumber))
            {
                purchasesBillHeaderList = purchasesBillHeaderList.Where(x => x.ClientVendor.PhoneNumber1 == searchCriteriaDTO.PersonPhoneNumber || x.ClientVendor.PhoneNumber2 == searchCriteriaDTO.PersonPhoneNumber);
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
            Treasury treasury = new Treasury()
            {
                Date = DateTime.Parse(entity.Date),
                AccountTypeId = AccountTypeEnum.Vendors,
                ClientVendorId = entity.ClientVendorId,
                BeneficiaryName = entity.ClientVendorName,
                //TransactionTypeId = TransactionTypeEnum.Incoming,
                PaymentMethodId = entity.PaymentMethodId,
                RefNo = entity.RefNo,
                IsBilled = true

            };

            if (entity.IsReturned)
            {
                treasury.Debit = entity.Paid;
                treasury.Credit = entity.TotalAfterDiscount;
                treasury.Notes = "فاتورة مرتجعات المشتريات";

            }
            else
            {
                treasury.Debit = entity.TotalAfterDiscount;
                treasury.Credit = entity.Paid;
                treasury.Notes = "فاتورة مشتريات";

            }

            return treasury;
        }

        public Task<long> Add(PurchasesBillHeaderDTO entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        private string GenerateSequenceNumber()
        {
            var lastElement = _unitOfWork.PurchasesBillHeaderDAL.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Number) + 1;
            return code.ToString();

        }

    }
}
