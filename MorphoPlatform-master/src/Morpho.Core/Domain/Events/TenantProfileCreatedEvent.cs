using Morpho.Domain.Common;

namespace Morpho.Domain.Events
{
    /// <summary>
    /// Domain event raised when a new tenant profile is created.
    /// </summary>
    public class TenantProfileCreatedEvent : DomainEvent
    {
        public int TenantId { get; }
        public string LegalName { get; }
        public string TradeName { get; }

        public TenantProfileCreatedEvent(int tenantId, string legalName, string tradeName)
        {
            TenantId = tenantId;
            LegalName = legalName;
            TradeName = tradeName;
        }
    }
}