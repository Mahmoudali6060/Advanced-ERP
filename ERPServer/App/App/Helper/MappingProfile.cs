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

            CreateMap<PortDTO, Port>();
            CreateMap<Port, PortDTO>();

            CreateMap<AdvertismentDTO, Advertisment>();
            CreateMap<Advertisment, AdvertismentDTO>();

            CreateMap<TrucksProvider, TrucksProviderDTO>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name));
            CreateMap<TrucksProviderDTO, TrucksProvider>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductDTO, Product>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            CreateMap<Vendor, VendorDTO>();
            CreateMap<VendorDTO, Vendor>();
            #endregion

            #region Users Management
            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));
            CreateMap<UserProfileDTO, UserProfile>();
            #endregion

            #region Purchases

            CreateMap<PurchasesBillHeader, PurchasesBillHeaderDTO>()
                .ForMember(dest => dest.PurchasesBillDetailList, opt => opt.MapFrom(src => src.PurchasesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.FullName));

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
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName));

            CreateMap<SalesBillHeaderDTO, SalesBillHeader>()
                .ForMember(dest => dest.SalesBillDetailList, opt => opt.MapFrom(src => src.SalesBillDetailList))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<SalesBillDetail, SalesBillDetailDTO>();
            CreateMap<SalesBillDetailDTO, SalesBillDetail>();
            #endregion
        }
    }
}