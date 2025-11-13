using System.Threading.Tasks;
using Abp.Application.Services;
using Morpho.Sessions.Dto;

namespace Morpho.Sessions
{
    /// <summary>
    ///  Application service to get current login information.
    /// </summary>
    public interface ISessionAppService : IApplicationService
    {
        /// <summary>
        ///  Gets current login information.
        /// </summary>
        /// <returns></returns>
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
