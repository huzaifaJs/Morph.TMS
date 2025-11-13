using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using Morpho.MultiTenancy;
using Abp.Localization;
using Morpho.Domain.Common;
using Morpho.Domain.Enums;

namespace Morpho.Domain.Entities
{
    public class TenantProfile : BaseEntity
    {
        public string LegalName { get; set; }
        public string TradeName { get; set; }
        public int CompanyTypeId { get; set; }
        public int IndustryId { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Lei { get; set; }
        public string EoriNumber { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string Website { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string Timezone { get; set; }
        public int? LanguageId { get; set; }
        public int? CurrencyId { get; set; }
        public ProfileStatus Status { get; set; }
        public string RiskLevel { get; set; }
        public string LogoUrl { get; set; }
        public string BrandColor { get; set; }
        public string Notes { get; set; }
        public int CountryId { get; set; }

        #region Navigation Properties
        public Tenant Tenant { get; set; }
        public CompanyType CompanyType { get; set; }
        public Industry Industry { get; set; }
        public ApplicationLanguage Language { get; set; }
        public Currency Currency { get; set; }
        public Country Country { get; set; }
        #endregion
    }
}
