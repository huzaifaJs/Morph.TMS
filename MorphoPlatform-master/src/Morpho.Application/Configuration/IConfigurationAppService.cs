using System.Threading.Tasks;
using Morpho.Configuration.Dto;

namespace Morpho.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
