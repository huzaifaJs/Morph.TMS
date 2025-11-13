using Abp.Domain.Entities;
using Morpho.Domain.Common;
using Morpho.Domain.Interfaces;

namespace Morpho.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string? Code { get; set; } // ISO Code, e.g., "QAR", "USD"
        public FullName Name { get; set; }
        public string? Symbol { get; set; } // e.g., "ر.ق", "$"
    }
}
