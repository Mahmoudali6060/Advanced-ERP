using AutoMapper;
using Data.Entities.Setup;
using Data.Entities.UserManagement;
using Entities.Account;
using Shared.Entities.Setup;

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
            #endregion

            #region Users Management
            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));
            CreateMap<UserProfileDTO, UserProfile>();
            #endregion

        }
    }
}