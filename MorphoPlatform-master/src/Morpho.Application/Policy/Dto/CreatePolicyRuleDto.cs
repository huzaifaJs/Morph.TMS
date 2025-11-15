using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Morpho.Domain.Entities.Policies;

namespace Morpho.Policy.Dto
{
    [AutoMapTo(typeof(PolicyRule))]
    public class CreatePolicyRuleDto
    {
        [Required]
        public Guid policy_id { get; set; }

        [Required]
        public int sensor_type { get; set; }

        [Required]
        public int condition_type { get; set; }

        public decimal? threshold_min { get; set; }
        public decimal? threshold_max { get; set; }
    }
}
