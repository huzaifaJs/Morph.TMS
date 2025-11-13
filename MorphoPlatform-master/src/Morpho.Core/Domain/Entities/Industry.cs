using Abp.Domain.Entities;
using Morpho.Domain.Common;

namespace Morpho.Domain.Entities
{
    public class Industry : Entity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
