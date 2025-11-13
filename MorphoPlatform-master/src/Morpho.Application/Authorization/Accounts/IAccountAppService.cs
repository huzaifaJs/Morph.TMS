using System.Threading.Tasks;
using Abp.Application.Services;
using Morpho.Authorization.Accounts.Dto;

namespace Morpho.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
