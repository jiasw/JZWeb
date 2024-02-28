using AutoMapper;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Dtos.Input;
using JZ.Domain.Entitys;

namespace JZ.Application.Contract
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JZ_YW_PIC, BannerDto>().ReverseMap();

            CreateMap<JZ_SYS_MANGER, MangerDto>().ReverseMap();

            CreateMap<JZ_YW_LXWM, LXWMDto>().ReverseMap();

            CreateMap<JZ_YW_DTGL, DTGLDto>().ReverseMap();

            CreateMap<JZ_YW_DTGL, DTXWListDto>().ReverseMap();

            CreateMap<JZ_SYS_ZDXX, ZDXXDto>().ReverseMap();

            CreateMap<JZ_YW_FWXX, FWXXDto>().ReverseMap();

            CreateMap<JZ_YW_FWXX, FWXXListDto>().ReverseMap();

            CreateMap<JZ_YW_ADDR, AddressDto>().ReverseMap();

            CreateMap<JZ_YW_ADDR, WXAddressInput>().ReverseMap();

            CreateMap<JZ_YW_ASSESS, AssessDto>().ReverseMap();

            CreateMap<JZ_YW_ORDER, OrderDto>().ReverseMap();

            CreateMap<JZ_YW_PAYINFO, PayInfoDto>().ReverseMap();

            CreateMap<JZ_YW_USERINFO, UserInfoDto>().ReverseMap();
        }
    }
}
