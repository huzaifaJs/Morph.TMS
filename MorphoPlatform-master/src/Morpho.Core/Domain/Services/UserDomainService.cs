using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Morpho.Authorization.Users;
using Morpho.Domain.Common;
using Morpho.Domain.Events;
using Morpho.Domain.Specifications;

namespace Morpho.Domain.Services
{
    /// <summary>
    /// Domain service for user-related business operations.
    /// </summary>
    public class UserDomainService : DomainService
    {
        private readonly IEventBus _eventBus;

        public UserDomainService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Determines if a user can be safely deleted based on business rules.
        /// </summary>
        /// <param name="user">The user to check</param>
        /// <returns>Result indicating if deletion is allowed</returns>
        public Result<bool> CanDeleteUser(User user)
        {
            if (user == null)
                return Result.Failure<bool>("User cannot be null");

            var deletableSpec = new DeletableUserSpecification();
            
            if (!deletableSpec.IsSatisfiedBy(user))
                return Result.Failure<bool>("User cannot be deleted: either admin user or inactive");

            // Additional business rules can be added here
            // For example: check if user has pending transactions, owns critical data, etc.

            return Result.Success(true);
        }

        /// <summary>
        /// Validates if a user can be activated based on business rules.
        /// </summary>
        /// <param name="user">The user to activate</param>
        /// <returns>Result indicating if activation is allowed</returns>
        public Result<bool> CanActivateUser(User user)
        {
            if (user == null)
                return Result.Failure<bool>("User cannot be null");

            if (user.IsActive)
                return Result.Failure<bool>("User is already active");

            if (!user.IsEmailConfirmed)
                return Result.Failure<bool>("User email must be confirmed before activation");

            return Result.Success(true);
        }

        /// <summary>
        /// Publishes user registration event.
        /// </summary>
        /// <param name="user">The registered user</param>
        /// <returns>Task representing the async operation</returns>
        public async Task PublishUserRegisteredEventAsync(User user)
        {
            var userRegisteredEvent = new UserRegisteredEvent(
                user.Id,
                user.UserName,
                user.EmailAddress,
                user.TenantId);

            await _eventBus.TriggerAsync(userRegisteredEvent);
        }
    }
}