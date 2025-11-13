using Abp.Domain.Entities;

namespace Morpho.Domain.Entities
{
    /// <summary>
    ///  Represents a type of company.
    /// </summary>
    public class CompanyType : Entity<int>
    {
        /// <summary>
        ///  Gets or sets the name of the company type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets the description of the company type.
        /// </summary>
        public string? Description { get; set; }
    }
}
