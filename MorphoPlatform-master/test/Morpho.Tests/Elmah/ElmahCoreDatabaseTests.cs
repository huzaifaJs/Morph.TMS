using System;
using System.Data;
using System.Threading.Tasks;
using ElmahCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Elmah
{
    /// <summary>
    /// Database integration tests for ElmahCore
    /// These tests verify that ElmahCore properly integrates with PostgreSQL database
    /// </summary>
    public class ElmahCoreDatabaseTests : MorphoTestBase
    {
        private readonly string _connectionString;

        public ElmahCoreDatabaseTests()
        {
            // Get connection string from configuration
            _connectionString = LocalIocManager.Resolve<IConfiguration>()
                .GetConnectionString("Default");
        }

        [Fact]
        public async Task Should_Create_ElmahCore_Table_In_Database()
        {
            // Arrange & Act
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            // Check if ELMAH_Error table exists
            var checkTableQuery = @"
                SELECT EXISTS (
                    SELECT FROM information_schema.tables 
                    WHERE table_schema = 'public' 
                    AND table_name = 'ELMAH_Error'
                );";

            using var command = new NpgsqlCommand(checkTableQuery, connection);
            var tableExists = (bool)await command.ExecuteScalarAsync();

            // Assert
            tableExists.ShouldBeTrue("ELMAH_Error table should exist in the database");
        }

        [Fact]
        public async Task Should_Insert_Error_Into_Database_When_Logged()
        {
            // Arrange
            var testMessage = $"Database integration test error - {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
            var testException = new InvalidOperationException(testMessage);

            // Get count before logging
            var countBefore = await GetErrorCountAsync();

            // Act
            ElmahExtensions.RaiseError(testException);

            // Wait a bit for async operations to complete
            await Task.Delay(100);

            // Get count after logging
            var countAfter = await GetErrorCountAsync();

            // Assert
            countAfter.ShouldBeGreaterThan(countBefore, "Error count should increase after logging an error");
        }

        [Fact]
        public async Task Should_Store_Error_Details_Correctly()
        {
            // Arrange
            var uniqueMessage = $"Detailed error test - {Guid.NewGuid()}";
            var testException = new ArgumentException(uniqueMessage, "testParameter");

            // Act
            ElmahExtensions.RaiseError(testException);

            // Wait for async operations
            await Task.Delay(100);

            // Assert - Check if error with our unique message exists
            var errorExists = await CheckErrorExistsByMessageAsync(uniqueMessage);
            errorExists.ShouldBeTrue($"Error with message '{uniqueMessage}' should exist in database");
        }

        [Fact]
        public async Task Should_Handle_High_Volume_Error_Logging()
        {
            // Arrange
            const int errorCount = 50;
            var initialCount = await GetErrorCountAsync();

            // Act - Log multiple errors quickly
            var tasks = new Task[errorCount];
            for (int i = 0; i < errorCount; i++)
            {
                int index = i; // Capture for closure
                tasks[i] = Task.Run(() =>
                {
                    var exception = new Exception($"High volume test error {index} - {DateTime.UtcNow.Ticks}");
                    ElmahExtensions.RaiseError(exception);
                });
            }

            await Task.WhenAll(tasks);
            await Task.Delay(500); // Wait for all async operations to complete

            // Assert
            var finalCount = await GetErrorCountAsync();
            finalCount.ShouldBeGreaterThanOrEqualTo(initialCount + errorCount, 
                "All errors should be logged to database");
        }

        [Fact]
        public async Task Should_Store_Stack_Trace_Information()
        {
            // Arrange
            var uniqueId = Guid.NewGuid().ToString();
            Exception exceptionWithStackTrace = null;

            try
            {
                GenerateExceptionWithStackTrace(uniqueId);
            }
            catch (Exception ex)
            {
                exceptionWithStackTrace = ex;
            }

            // Act
            ElmahExtensions.RaiseError(exceptionWithStackTrace);
            await Task.Delay(100);

            // Assert
            var hasStackTrace = await CheckErrorHasStackTraceAsync(uniqueId);
            hasStackTrace.ShouldBeTrue("Logged error should contain stack trace information");
        }

        [Fact]
        public async Task Should_Store_Exception_Type_Information()
        {
            // Arrange
            var uniqueMessage = $"Type test - {Guid.NewGuid()}";
            var testException = new NotImplementedException(uniqueMessage);

            // Act
            ElmahExtensions.RaiseError(testException);
            await Task.Delay(100);

            // Assert
            var correctType = await CheckErrorTypeAsync(uniqueMessage, "NotImplementedException");
            correctType.ShouldBeTrue("Error should store correct exception type");
        }

        [Fact]
        public async Task Should_Store_Timestamp_Information()
        {
            // Arrange
            var beforeLogging = DateTime.UtcNow;
            var uniqueMessage = $"Timestamp test - {Guid.NewGuid()}";
            var testException = new Exception(uniqueMessage);

            // Act
            ElmahExtensions.RaiseError(testException);
            await Task.Delay(100);

            var afterLogging = DateTime.UtcNow;

            // Assert
            var timestamp = await GetErrorTimestampAsync(uniqueMessage);
            timestamp.ShouldNotBeNull("Error should have timestamp");
            timestamp.Value.ShouldBeGreaterThanOrEqualTo(beforeLogging.AddSeconds(-1)); // Allow 1 second tolerance
            timestamp.Value.ShouldBeLessThanOrEqualTo(afterLogging.AddSeconds(1)); // Allow 1 second tolerance
        }

        #region Private Helper Methods

        private async Task<int> GetErrorCountAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM \"ELMAH_Error\"";
            using var command = new NpgsqlCommand(query, connection);
            
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        private async Task<bool> CheckErrorExistsByMessageAsync(string message)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM \"ELMAH_Error\" WHERE \"Message\" LIKE @message";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@message", $"%{message}%");

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        private async Task<bool> CheckErrorHasStackTraceAsync(string uniqueId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT COUNT(*) FROM ""ELMAH_Error"" 
                         WHERE ""Message"" LIKE @uniqueId 
                         AND ""AllXml"" IS NOT NULL 
                         AND ""AllXml"" != ''";
            
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@uniqueId", $"%{uniqueId}%");

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        private async Task<bool> CheckErrorTypeAsync(string message, string expectedType)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT COUNT(*) FROM ""ELMAH_Error"" 
                         WHERE ""Message"" LIKE @message 
                         AND ""Type"" LIKE @type";
            
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@message", $"%{message}%");
            command.Parameters.AddWithValue("@type", $"%{expectedType}%");

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        private async Task<DateTime?> GetErrorTimestampAsync(string message)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT ""TimeUtc"" FROM ""ELMAH_Error"" 
                         WHERE ""Message"" LIKE @message 
                         ORDER BY ""TimeUtc"" DESC 
                         LIMIT 1";
            
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@message", $"%{message}%");

            var result = await command.ExecuteScalarAsync();
            return result as DateTime?;
        }

        private void GenerateExceptionWithStackTrace(string uniqueId)
        {
            Level1Method(uniqueId);
        }

        private void Level1Method(string uniqueId)
        {
            Level2Method(uniqueId);
        }

        private void Level2Method(string uniqueId)
        {
            throw new InvalidOperationException($"Stack trace test exception - {uniqueId}");
        }

        #endregion
    }
}
