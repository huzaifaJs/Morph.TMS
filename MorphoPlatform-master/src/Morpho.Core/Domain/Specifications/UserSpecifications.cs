using System;
using System.Linq.Expressions;
using Morpho.Authorization.Users;

namespace Morpho.Domain.Specifications
{
    /// <summary>
    /// Specification to check if a user is active and verified.
    /// </summary>
    public class ActiveUserSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> Criteria =>
            user => user.IsActive && user.IsEmailConfirmed;
    }

    /// <summary>
    /// Specification to check if a user belongs to a specific tenant.
    /// </summary>
    public class UserBelongsToTenantSpecification : Specification<User>
    {
        private readonly int _tenantId;

        public UserBelongsToTenantSpecification(int tenantId)
        {
            _tenantId = tenantId;
        }

        public override Expression<Func<User, bool>> Criteria =>
            user => user.TenantId == _tenantId;
    }

    /// <summary>
    /// Specification to check if a user can be deleted (not admin user and currently active).
    /// </summary>
    public class DeletableUserSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> Criteria =>
            user => user.UserName != User.AdminUserName && user.IsActive;
    }

    /// <summary>
    /// Specification to check if a user has been recently created.
    /// </summary>
    public class RecentlyCreatedUserSpecification : Specification<User>
    {
        private readonly DateTime _threshold;

        public RecentlyCreatedUserSpecification(int daysThreshold = 30)
        {
            _threshold = DateTime.UtcNow.AddDays(-daysThreshold);
        }

        public override Expression<Func<User, bool>> Criteria =>
            user => user.CreationTime >= _threshold;
    }
}