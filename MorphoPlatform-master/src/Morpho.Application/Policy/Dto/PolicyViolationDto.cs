using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.Policies;
using System;

namespace Morpho.Policy.Dto
{
    [AutoMapFrom(typeof(PolicyViolation))]
    public class PolicyViolationDto : EntityDto<Guid>
    {
        public int tenant_id { get; set; }
        public Guid device_id { get; set; }
        public Guid policy_id { get; set; }
        public Guid rule_id { get; set; }

        public int sensor_type { get; set; }
        public decimal value { get; set; }
        public string unit { get; set; }

        public DateTime occurred_at_utc { get; set; }
        public Guid telemetry_id { get; set; }
    }
}
