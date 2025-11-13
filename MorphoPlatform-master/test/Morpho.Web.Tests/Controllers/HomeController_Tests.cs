using System.Threading.Tasks;
using Morpho.Models.TokenAuth;
using Morpho.Web.Controllers;
using Shouldly;
using Xunit;

namespace Morpho.Web.Tests.Controllers
{
    public class HomeController_Tests: MorphoWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}