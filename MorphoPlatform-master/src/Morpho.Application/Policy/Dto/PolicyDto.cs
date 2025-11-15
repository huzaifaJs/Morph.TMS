using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// entity alias
using PolicyEntity = Morpho.Domain.Entities.Policies.Policy;

namespace Morpho.Policy.Dto
{
    [AutoMapFrom(typeof(PolicyEntity))]
    public class PolicyDto : EntityDto<Guid>
    {
        [Required]
        public int tenant_id { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [MaxLength(1000)]
        public string description { get; set; }

        public bool is_active { get; set; }

        public List<PolicyRuleDto> rules { get; set; }
    }
}
