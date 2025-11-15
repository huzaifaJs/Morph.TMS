using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using PolicyEntity = Morpho.Domain.Entities.Policies.Policy;

namespace Morpho.Policy.Dto
{
    [AutoMapTo(typeof(PolicyEntity))]
    public class CreatePolicyDto
    {
        [Required]
        public int tenant_id { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [MaxLength(1000)]
        public string description { get; set; }
    }
}
