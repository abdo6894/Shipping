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
            CreateMap<TbCity, TbCityDto>().ReverseMap();
            CreateMap<VwCitiy, TbCityDto>().ReverseMap();
            CreateMap<TbCountry, TbCountryDto>().ReverseMap();
            CreateMap<TbPaymentMethod, TbPaymentMethodDto>().ReverseMap();
            CreateMap<TbSetting, TbSettingDto>().ReverseMap();
            CreateMap<TbShippingType, TbShippingTypeDto>().ReverseMap();
            CreateMap<TbShippment, TbShippmentDto>().ReverseMap();
            CreateMap<TbShippmentStatus, TbShippmentStatusDto>().ReverseMap();
            CreateMap<TbSubscriptionPackage, TbSubscriptionPackageDto>().ReverseMap();
            CreateMap<TbUserReciver, TbUserReciverDto>().ReverseMap();
            CreateMap<TbUserSender, TbUserSenderDto>().ReverseMap();
            CreateMap<TbUserSubscription, TbUserSubscriptionDto>().ReverseMap();
        }


    }
}
