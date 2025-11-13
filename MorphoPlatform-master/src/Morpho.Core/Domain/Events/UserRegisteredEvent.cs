using Morpho.Domain.Common;

namespace Morpho.Domain.Events
{
    /// <summary>
    /// Domain event raised when a user is successfully registered.
    /// </summary>
    public class UserRegisteredEvent : DomainEvent
    {
        public long UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public int? TenantId { get; }

        public UserRegisteredEvent(long userId, string userName, string email, int? tenantId)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            TenantId = tenantId;
        }
    }
}