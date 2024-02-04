using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Setup;
using Data.Entities.Setup;
using DataService.Setup.Contracts;
using System;
using Shared.Enums;
using Data.Contexts;
using Data.Entities.Accouting;
using Shared.Entities.Sales;

namespace DataService.Setup.Handlers
{
    public class ClientVendorDSL : IClientVendorDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public ClientVendorDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<ClientVendorDTO>> GetAll(ClientVendorSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.ClientVendorDAL.GetAllAsync();

            #region Apply Filters
            userProfileList = userProfileList.OrderBy(x => x.FullName);
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            int total = userProfileList.Count();
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<ClientVendorDTO>>(userProfileList);
            return new ResponseEntityList<ClientVendorDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ClientVendorDTO> GetById(long id)
        {
            return _mapper.Map<ClientVendorDTO>(await _unitOfWork.ClientVendorDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<ClientVendorDTO>> GetAllLite()
        {
            return new ResponseEntityList<ClientVendorDTO>()
            {
                List = _mapper.Map<IEnumerable<ClientVendorDTO>>(_unitOfWork.ClientVendorDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.ClientVendorDAL.GetAllLiteAsync().Result.Count()
            };
        }

        public async Task<ResponseEntityList<ClientVendorDTO>> GetAllLiteByTypeId(ClientVendorTypeEnum typeId)
        {
            return new ResponseEntityList<ClientVendorDTO>()
            {
                List = _mapper.Map<IEnumerable<ClientVendorDTO>>(_unitOfWork.ClientVendorDAL.GetAllLiteAsync().Result.Where(x => x.TypeId == typeId || x.TypeId == ClientVendorTypeEnum.All)),
                Total = _unitOfWork.ClientVendorDAL.GetAllLiteAsync().Result.Count(x => x.TypeId == typeId)
            };
        }


        #endregion

        #region Command
        public async Task<long> Add(ClientVendorDTO entityDTO)
        {
            entityDTO.Code = GenerateSequenceNumber();
            UploadClientVendorImage(entityDTO);
            entityDTO.OppeningBalance = entityDTO.Debit - entityDTO.Credit;
            var entity = _mapper.Map<ClientVendor>(entityDTO);
            entity.AccountStatementList = MapAccountStatement(entity);
            await _unitOfWork.ClientVendorDAL.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<long> Update(ClientVendorDTO entity)
        {
            UploadClientVendorImage(entity);
            var result = await _unitOfWork.ClientVendorDAL.UpdateAsync(_mapper.Map<ClientVendor>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            ClientVendor entity = await _unitOfWork.ClientVendorDAL.GetByIdAsync(id);
            var result = await _unitOfWork.ClientVendorDAL.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<ClientVendor> ApplyFilert(IQueryable<ClientVendor> clientList, ClientVendorSearchDTO searchCriteriaDTO)
        {
            clientList = clientList.Where(x => x.TypeId == searchCriteriaDTO.TypeId || x.TypeId == ClientVendorTypeEnum.All);

            if (searchCriteriaDTO.IsActive.HasValue)
            {
                clientList = clientList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                clientList = clientList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.FullName))
            {
                clientList = clientList.Where(x => x.FullName.Contains(searchCriteriaDTO.FullName));
            }
            return clientList;
        }

        private bool UploadClientVendorImage(ClientVendorDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.FullName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/ClientVendors/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }

        private string GenerateSequenceNumber()
        {
            var lastElement = _unitOfWork.ClientVendorDAL.GetAll().OrderBy(p => p.Id)
                       .FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Code) + 1;
            return code.ToString();
        }


        private List<AccountStatement> MapAccountStatement(ClientVendor entity)
        {
            List<AccountStatement> accountStatement = new List<AccountStatement>()
            {
                new AccountStatement()
                {
                     Date = DateTime.Now.Date,
                     BeneficiaryName = entity.FullName,
                     PaymentMethodId = PaymentMethodEnum.Cash,
                     //RefNo = entity.RefNo,
                     IsBilled = true,
                     Debit = entity.Debit,
                     Credit = entity.Credit,
                     Notes = "رصيد افتتاحي"
                }
            };
            return accountStatement;
        }

        #endregion
    }
}
