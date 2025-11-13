using Abp.Authorization;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;

namespace Morpho.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
