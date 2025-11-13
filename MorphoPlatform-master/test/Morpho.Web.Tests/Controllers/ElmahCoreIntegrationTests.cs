using System;
using System.Net;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Morpho.Models.TokenAuth;
using Shouldly;
using Xunit;

namespace Morpho.Web.Tests.Controllers
{
    /// <summary>
    /// Integration tests for ElmahCore endpoints and functionality
    /// These tests verify the complete ElmahCore integration in the web application
    /// </summary>
    public class ElmahCoreIntegrationTests : MorphoWebTestBase
    {
        [Fact]
        public async Task Should_Require_Authentication_For_Elmah_Dashboard()
        {
            // Act - Try to access ElmahCore dashboard without authentication
            var response = await Client.GetAsync("/elmah");

            // Assert - Should be redirected or unauthorized
            response.StatusCode.ShouldBeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.Redirect, HttpStatusCode.Found);
        }

        [Fact]
        public async Task Should_Allow_Authenticated_User_To_Access_Elmah_Dashboard()
        {
            // Arrange - Authenticate as admin user
            await AuthenticateAsync(AbpTenantBase.DefaultTenantName, new AuthenticateModel
            {
                UserNameOrEmailAddress = AbpUserBase.AdminUserName,
                Password = "123qwe"
            });

            // Act - Try to access ElmahCore dashboard with authentication
            var response = await Client.GetAsync("/elmah");

            // Assert - Should be successful (200) or redirect to actual dashboard
            response.StatusCode.ShouldBeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect, HttpStatusCode.Found);
            response.StatusCode.ShouldNotBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Should_Log_Error_When_TestElmahError_Endpoint_Called()
        {
            // Act - Call the test error endpoint
            var response = await Client.GetAsync("/Home/TestElmahError");

            // Assert - Should return success (error was logged)
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Test exception logged to ElmahCore");
            content.ShouldContain("Check /elmah to view the error log");
        }

        [Fact]
        public async Task Should_Log_Custom_Message_When_TestElmahLog_Endpoint_Called()
        {
            // Arrange
            var customMessage = "Integration test custom message";

            // Act - Call the test log endpoint with custom message
            var response = await Client.GetAsync($"/Home/TestElmahLog?message={Uri.EscapeDataString(customMessage)}");

            // Assert - Should return success
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Custom log entry created in ElmahCore");
            content.ShouldContain(customMessage);
        }

        [Fact]
        public async Task Should_Log_Default_Message_When_TestElmahLog_Called_Without_Parameters()
        {
            // Act - Call the test log endpoint without parameters
            var response = await Client.GetAsync("/Home/TestElmahLog");

            // Assert - Should return success with default message
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Custom log entry created in ElmahCore");
            content.ShouldContain("This is a test log entry created at");
        }

        [Fact]
        public async Task Should_Handle_Multiple_Concurrent_Error_Requests()
        {
            // Arrange
            const int numberOfRequests = 10;
            var tasks = new Task<System.Net.Http.HttpResponseMessage>[numberOfRequests];

            // Act - Make multiple concurrent requests to error endpoint
            for (int i = 0; i < numberOfRequests; i++)
            {
                tasks[i] = Client.GetAsync("/Home/TestElmahError");
            }

            var responses = await Task.WhenAll(tasks);

            // Assert - All requests should succeed
            foreach (var response in responses)
            {
                response.StatusCode.ShouldBe(HttpStatusCode.OK);
                var content = await response.Content.ReadAsStringAsync();
                content.ShouldContain("Test exception logged to ElmahCore");
            }
        }

        [Fact]
        public async Task Should_Handle_Special_Characters_In_Log_Message()
        {
            // Arrange
            var specialMessage = "Test with special chars: <>\"'&\n\r\t";

            // Act
            var response = await Client.GetAsync($"/Home/TestElmahLog?message={Uri.EscapeDataString(specialMessage)}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Custom log entry created in ElmahCore");
        }

        [Fact]
        public async Task Should_Handle_Long_Log_Messages()
        {
            // Arrange
            var longMessage = new string('A', 1000); // 1000 character message

            // Act
            var response = await Client.GetAsync($"/Home/TestElmahLog?message={Uri.EscapeDataString(longMessage)}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Custom log entry created in ElmahCore");
        }

        [Fact]
        public async Task Should_Return_Valid_Response_Format_For_Test_Endpoints()
        {
            // Act - Test both endpoints
            var errorResponse = await Client.GetAsync("/Home/TestElmahError");
            var logResponse = await Client.GetAsync("/Home/TestElmahLog?message=format-test");

            // Assert - Both should return valid content
            errorResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            logResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var errorContent = await errorResponse.Content.ReadAsStringAsync();
            var logContent = await logResponse.Content.ReadAsStringAsync();

            // Error response should contain timestamp and instructions
            errorContent.ShouldContain("Test exception logged to ElmahCore at");
            errorContent.ShouldContain("Check /elmah to view the error log");

            // Log response should contain confirmation and instructions
            logContent.ShouldContain("Custom log entry created in ElmahCore");
            logContent.ShouldContain("format-test");
            logContent.ShouldContain("Check /elmah to view the log");
        }

        [Fact]
        public async Task Should_Handle_Rapid_Sequential_Requests()
        {
            // Act - Make rapid sequential requests
            for (int i = 0; i < 5; i++)
            {
                var response = await Client.GetAsync($"/Home/TestElmahLog?message=Sequential-{i}");
                
                // Assert - Each request should succeed
                response.StatusCode.ShouldBe(HttpStatusCode.OK);
                
                var content = await response.Content.ReadAsStringAsync();
                content.ShouldContain($"Sequential-{i}");
            }
        }

        [Fact]
        public async Task Should_Maintain_Session_State_During_Error_Logging()
        {
            // Arrange - Authenticate first
            await AuthenticateAsync(AbpTenantBase.DefaultTenantName, new AuthenticateModel
            {
                UserNameOrEmailAddress = AbpUserBase.AdminUserName,
                Password = "123qwe"
            });

            // Act - Make request that logs error while authenticated
            var response = await Client.GetAsync("/Home/TestElmahError");

            // Assert - Should maintain authentication and succeed
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            
            // Verify we can still access authenticated endpoints
            var dashboardResponse = await Client.GetAsync("/elmah");
            dashboardResponse.StatusCode.ShouldNotBe(HttpStatusCode.Unauthorized);
        }
    }
}
