using Abp.AutoMapper;
using Morpho.Domain.Entities;
using System;

namespace Morpho.Services.TenantDocuments.Dtos
{
    [AutoMapTo(typeof(TenantDocument))]
    public class CreateTenantDocumentDto
    {
        public int TenantId { get; set; }
        public string DocType { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; }
        public string StorageKey { get; set; }
        public string? MimeType { get; set; }
        public string? FileHash { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? Version { get; set; }
        public bool IsCurrent { get; set; }
        public string Status { get; set; } 
    }
}
