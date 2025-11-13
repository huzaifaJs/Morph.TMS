using Abp.Domain.Entities;
using System;
using Morpho.Domain.Enums;
using Morpho.MultiTenancy;
using Morpho.Authorization.Users;
using Morpho.Domain.Common;

namespace Morpho.Domain.Entities
{
    public class TenantDocument : Entity
    {
        public int TenantId { get; set; }
        public DocumentType DocType { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string StorageKey { get; set; }
        public MimeType? MimeType { get; set; }
        public string? FileHash { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? Version { get; set; }
        public bool IsCurrent { get; set; }
        public DocumentStatus Status { get; set; }

        #region Navigation Properties
        public Tenant Tenant { get; set; }
        #endregion
    }

}
