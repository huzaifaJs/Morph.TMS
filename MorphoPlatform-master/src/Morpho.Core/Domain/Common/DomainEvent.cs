using System;
using Abp.Events.Bus;

namespace Morpho.Domain.Common
{
    /// <summary>
    /// Base class for domain events in the system.
    /// </summary>
    public abstract class DomainEvent : EventData
    {
        public DateTime OccurredOn { get; private set; }

        protected DomainEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }
}