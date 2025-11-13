using Abp.Domain.Entities;
using System.Collections.Generic;
using Morpho.Domain.Common;

namespace Morpho.Domain.Common
{
    /// <summary>
    /// Base class for aggregate roots that can publish domain events.
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    /// <summary>
    /// Base class for aggregate roots with typed identifier that can publish domain events.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    /// <summary>
    /// Interface for aggregate roots.
    /// </summary>
    public interface IAggregateRoot
    {
        IReadOnlyList<DomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }

    /// <summary>
    /// Interface for aggregate roots with typed identifier.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IAggregateRoot
    {
    }
}