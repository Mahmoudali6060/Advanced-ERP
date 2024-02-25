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

namespace DataService.Setup.Handlers
{
    public class DashboardDSL : IDashboardDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public DashboardDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<DashboardDTO> GetDashboard(DashboardSearchDTO searchCrieria)
        {
            return new DashboardDTO()
            {
                ClientsCount = _unitOfWork.ClientVendorDAL.GetAsync(c =>  c.TypeId == ClientVendorTypeEnum.Client || c.TypeId == ClientVendorTypeEnum.All).Result.Count(),
                VendorsCount = _unitOfWork.ClientVendorDAL.GetAsync(c =>  c.TypeId == ClientVendorTypeEnum.Vendor || c.TypeId == ClientVendorTypeEnum.All).Result.Count(),
                ProductsCount = _unitOfWork.ProductDAL.GetAsync(x => x.IsActive == true).Result.Count(),
                UsersCount = _unitOfWork.UserProfileDAL.GetAllLiteAsync().Result.Count()
            };
        }
    }
}
