using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.Policies;
using System;

namespace Morpho.Policy.Dto
{
    [AutoMapFrom(typeof(PolicyRule))]
    public class PolicyRuleDto : EntityDto<Guid>
    {
        public Guid policy_id { get; set; }
        public int sensor_type { get; set; }
        public int condition_type { get; set; }

        public decimal? threshold_min { get; set; }
        public decimal? threshold_max { get; set; }
    }
}
