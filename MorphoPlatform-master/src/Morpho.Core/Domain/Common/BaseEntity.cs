using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Morpho.Domain.Common;

public class BaseEntity : FullAuditedEntity, IMayHaveTenant
{
    public int? TenantId { get; set; }
}
