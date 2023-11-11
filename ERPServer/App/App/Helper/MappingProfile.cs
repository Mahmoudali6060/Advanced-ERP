using AutoMapper;
using Data.Entities.Purchases;
using Data.Entities.Sales;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Entities.Account;
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

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<ClientVendor, ClientVendorDTO>();
            CreateMap<ClientVendorDTO, ClientVendor>();

            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

            #endregion

            #region Users Management
            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));


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
                .ForMember(dest => dest.ClientVendorName, opt => opt.MapFrom(src => src.ClientVendor.FullName));

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
                .ForMember(dest => dest.ClientVendorName, opt => opt.MapFrom(src => src.ClientVendor.FullName));

            CreateMap<SalesBillHeaderDTO, SalesBillHeader>()
                .ForMember(dest => dest.SalesBillDetailList, opt => opt.MapFrom(src => src.SalesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<SalesBillDetail, SalesBillDetailDTO>();
            CreateMap<SalesBillDetailDTO, SalesBillDetail>();
            #endregion
        }
    }
}