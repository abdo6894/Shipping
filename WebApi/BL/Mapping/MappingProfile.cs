using AutoMapper;
using BL.Dtos;
using Domains;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<VwCitiy, CityDto>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Carrier, CarrierDto>().ReverseMap();
        CreateMap<Setting, SettingDto>().ReverseMap();
        CreateMap<ShipingType, ShipingTypeDto>().ReverseMap();
        CreateMap<ShipingPackging, ShipingPackgingDto>().ReverseMap();

        // Shipment
        CreateMap<Shipment, ShipmentDto>()
         .ForMember(dest => dest.SenderData, opt => opt.MapFrom(src => src.Sender))
         .ForMember(dest => dest.ReciverData, opt => opt.MapFrom(src => src.Receiver))
         .ReverseMap()
         .ForMember(dest => dest.CurrentState, opt => opt.Ignore());

        // UserSender
        CreateMap<UserSender, UserSenderDto>()
            .ForMember(dest => dest.CountryId,
                       opt => opt.MapFrom(src => src.City != null ? src.City.CountryId : Guid.Empty))
            .ReverseMap();

        // UserReciver
        CreateMap<UserReciver, UserReciverDto>()
            .ForMember(dest => dest.CountryId,
                       opt => opt.MapFrom(src => src.City != null ? src.City.CountryId : Guid.Empty))
            .ReverseMap();

        CreateMap<ShipmentStatus, ShipmentStatusDto>().ReverseMap();
        CreateMap<SubscriptionPackage, SubscriptionPackageDto>().ReverseMap();
        CreateMap<UserSubscription, UserSubscriptionDto>().ReverseMap();
    }
}
