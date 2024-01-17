using AutoMapper;
using Data.Entities.Accouting;
using Data.Entities.Purchases;
using Data.Entities.Sales;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Entities.Account;
using Shared.Entities.Accouting;
using Shared.Entities.Purchases;
using Shared.Entities.Sales;
using Shared.Entities.Setup;
using System;

namespace App.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Setup
            CreateMap<CountryDTO, Country>();
            CreateMap<Country, CountryDTO>();

            CreateMap<StateDTO, State>();
            CreateMap<State, StateDTO>();

            CreateMap<CityDTO, City>();
            CreateMap<City, CityDTO>();

            CreateMap<AdvertismentDTO, Advertisment>();
            CreateMap<Advertisment, AdvertismentDTO>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductDTO, Product>();


            CreateMap<ProductTracking, ProductTrackingDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd hh:mm tt")));
            CreateMap<ProductTrackingDTO, ProductTracking>();


            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<ClientVendor, ClientVendorDTO>();
            CreateMap<ClientVendorDTO, ClientVendor>();

            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

            CreateMap<Representive, RepresentiveDTO>();
            CreateMap<RepresentiveDTO, Representive>();

            CreateMap<UnitOfMeasurement, UnitOfMeasurementDTO>();
            CreateMap<UnitOfMeasurementDTO, UnitOfMeasurement>();

            #endregion

            #region Users Management
            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.CompanyDTO, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.RoleGroupDTO, opt => opt.MapFrom(src => src.Role));



            CreateMap<UserProfileDTO, UserProfile>();

            CreateMap<RoleGroup, RoleGroupDTO>()
                .ForMember(dest => dest.RolePrivileges, opt => opt.MapFrom(src => src.RolePrivileges));
            CreateMap<RoleGroupDTO, RoleGroup>()
                .ForMember(dest => dest.RolePrivileges, opt => opt.MapFrom(src => src.RolePrivileges));

            CreateMap<RolePrivilege, RolePrivilegeDTO>();
            CreateMap<RolePrivilegeDTO, RolePrivilege>();
            #endregion

            #region Purchases

            CreateMap<PurchasesBillHeader, PurchasesBillHeaderDTO>()
                .ForMember(dest => dest.PurchasesBillDetailList, opt => opt.MapFrom(src => src.PurchasesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.ClientVendorName, opt => opt.MapFrom(src => src.ClientVendor.FullName))
                .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUsername))
                .ForMember(dest => dest.ModifiedByUsername, opt => opt.MapFrom(src => src.ModifiedByUsername))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.AccountStatement.PaymentMethodId))
                .ForMember(dest => dest.RefNo, opt => opt.MapFrom(src => src.AccountStatement.RefNo));


            CreateMap<PurchasesBillHeaderDTO, PurchasesBillHeader>()
                .ForMember(dest => dest.PurchasesBillDetailList, opt => opt.MapFrom(src => src.PurchasesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<PurchasesBillDetail, PurchasesBillDetailDTO>();
            CreateMap<PurchasesBillDetailDTO, PurchasesBillDetail>();
            #endregion

            #region Sales

            CreateMap<SalesBillHeader, SalesBillHeaderDTO>()
                .ForMember(dest => dest.SalesBillDetailList, opt => opt.MapFrom(src => src.SalesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.ClientVendorName, opt => opt.MapFrom(src => src.ClientVendor.FullName))
                .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUsername))
                .ForMember(dest => dest.ModifiedByUsername, opt => opt.MapFrom(src => src.ModifiedByUsername))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.AccountStatement.PaymentMethodId))
                .ForMember(dest => dest.RefNo, opt => opt.MapFrom(src => src.AccountStatement.RefNo));



            CreateMap<SalesBillHeaderDTO, SalesBillHeader>()
                .ForMember(dest => dest.SalesBillDetailList, opt => opt.MapFrom(src => src.SalesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<SalesBillDetail, SalesBillDetailDTO>();
            CreateMap<SalesBillDetailDTO, SalesBillDetail>();
            #endregion

            #region Accounting
            CreateMap<AccountStatement, AccountStatementDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.BeneficiaryName, opt => opt.MapFrom(src => src.ClientVendor != null ? src.ClientVendor.FullName : src.BeneficiaryName));

            CreateMap<AccountStatementDTO, AccountStatement>();

            CreateMap<Treasury, TreasuryDTO>()
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
               .ForMember(dest => dest.BeneficiaryName, opt => opt.MapFrom(src => src.ClientVendor != null ? src.ClientVendor.FullName : src.BeneficiaryName));

            CreateMap<TreasuryDTO, Treasury>();

            #endregion
        }
    }
}