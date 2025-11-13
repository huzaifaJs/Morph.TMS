using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using Morpho.Sessions.Dto;

namespace Morpho.Sessions
{
    /// <summary>
    ///  Application service to get current login information.
    /// </summary>
    public class SessionAppService : MorphoAppServiceBase, ISessionAppService
    {
        /// <summary>
        ///  Gets current login information.
        /// </summary>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
