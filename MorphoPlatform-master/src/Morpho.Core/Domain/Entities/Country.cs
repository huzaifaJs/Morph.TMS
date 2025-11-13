using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Morpho.Domain.Interfaces;
using System;
using Morpho.Domain.Common;

namespace Morpho.Domain.Entities
{
    public class Country : Entity<int>
    {
        public string? Code { get; set; }
        public FullName Name { get; set; }
        public string? PhoneCode { get; set; }
    }
}
