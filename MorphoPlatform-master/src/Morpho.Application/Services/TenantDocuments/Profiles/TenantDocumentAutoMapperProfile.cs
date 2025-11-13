using System;
using AutoMapper;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;
using Morpho.Services.TenantDocuments.Dtos;

namespace Morpho.Services.TenantDocuments.Profiles
{

    public class TenantDocumentAutoMapperProfile : Profile
    {
        public TenantDocumentAutoMapperProfile()
        {
            // Entity -> DTO (enum -> string)
            CreateMap<TenantDocument, TenantDocumentDto>()
                .ForMember(dest => dest.DocType,
                           opt => opt.MapFrom(src => src.DocType.ToString()))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.MimeType,
                            opt => opt.MapFrom(src => src.MimeType.HasValue ? src.MimeType.Value.ToString() : null));

            // Create DTO -> Entity (string -> enum)
            CreateMap<CreateTenantDocumentDto, TenantDocument>()
                .ForMember(dest => dest.DocType,
                           opt => opt.MapFrom(src => (DocumentType)Enum.Parse(typeof(DocumentType), src.DocType, true)))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => (DocumentStatus)Enum.Parse(typeof(DocumentStatus), src.Status, true)))
                .ForMember(dest => dest.MimeType,
                         opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MimeType) ? (MimeType?)null : (MimeType)Enum.Parse(typeof(MimeType), src.MimeType, true)));

            // Update DTO -> Entity (string -> enum), keep Id mapping, etc.
            CreateMap<UpdateTenantDocumentDto, TenantDocument>()
                .ForMember(dest => dest.DocType,
                           opt => opt.MapFrom(src => (DocumentType)Enum.Parse(typeof(DocumentType), src.DocType, true)))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => (DocumentStatus)Enum.Parse(typeof(DocumentStatus), src.Status, true)))
                .ForMember(dest => dest.MimeType,
                           opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MimeType) ? (MimeType?)null : (MimeType)Enum.Parse(typeof(MimeType), src.MimeType, true)));
        }
    }

}
