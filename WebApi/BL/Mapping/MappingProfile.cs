using AutoMapper;
using BL.Dtos;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City,CityDto>().ReverseMap();
            CreateMap<VwCitiy, CityDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();
            CreateMap<ShipingType, ShipingTypeDto>().ReverseMap();
            CreateMap<ShipingPackging, ShipingPackgingDto>().ReverseMap();
            CreateMap<Shipment, ShipmentDto>().ReverseMap();
            CreateMap<ShipmentStatus, ShipmentStatusDto>().ReverseMap();
            CreateMap<SubscriptionPackage, SubscriptionPackageDto>().ReverseMap();
            CreateMap<UserReciver, UserReciverDto>().ReverseMap();
            CreateMap<UserSender, UserSenderDto>().ReverseMap();
            CreateMap<UserSubscription, UserSubscriptionDto>().ReverseMap();
        }


    }
}
