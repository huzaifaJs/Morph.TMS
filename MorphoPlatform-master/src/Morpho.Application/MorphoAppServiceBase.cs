using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Morpho.Authorization.Users;
using Morpho.MultiTenancy;

namespace Morpho
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MorphoAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected MorphoAppServiceBase()
        {
            LocalizationSourceName = MorphoConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var userId = AbpSession.GetUserId();
            if (userId == 0)
            {
                throw new UserFriendlyException("No authenticated user found.");
            }

            var user = await UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserFriendlyException("Current user not found in the system.");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
