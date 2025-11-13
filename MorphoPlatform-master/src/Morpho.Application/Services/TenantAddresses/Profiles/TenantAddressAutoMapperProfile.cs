using AutoMapper;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;
using Morpho.Services.TenantAddresses.Dtos;

namespace Morpho.Services.TenantAddresses.Profiles
{
    public class TenantAddressAutoMapperProfile : Profile
    {
        public TenantAddressAutoMapperProfile()
        {
            // Entity -> DTO (enum -> string)
            CreateMap<TenantAddress, TenantAddressDto>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src => src.Type.ToString()));

            // Create DTO -> Entity (string -> enum)
            CreateMap<CreateTenantAddressDto, TenantAddress>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src => (TenantAddressType)System.Enum.Parse(typeof(TenantAddressType), src.Type, true)));

            // Update DTO -> Entity (string -> enum), keep Id mapping, etc.
            CreateMap<UpdateTenantAddressDto, TenantAddress>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src => (TenantAddressType)System.Enum.Parse(typeof(TenantAddressType), src.Type, true)));
        }
    }
}
