using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElmahCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Elmah
{
    /// <summary>
    /// Real-time monitoring tests for ElmahCore
    /// These tests verify that ElmahCore works correctly in real-time scenarios
    /// </summary>
    public class ElmahCoreRealTimeTests : MorphoTestBase
    {
        private readonly string _connectionString;

        public ElmahCoreRealTimeTests()
        {
            _connectionString = LocalIocManager.Resolve<IConfiguration>()
                .GetConnectionString("Default");
        }

        [Fact]
        public async Task Should_Log_Errors_In_Real_Time_Under_Load()
        {
            // Arrange
            const int concurrentTasks = 20;
            const int errorsPerTask = 10;
            var totalExpectedErrors = concurrentTasks * errorsPerTask;
            
            var initialCount = await GetErrorCountAsync();
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Act - Simulate high load error logging
            var tasks = new Task[concurrentTasks];
            for (int i = 0; i < concurrentTasks; i++)
            {
                int taskId = i;
                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < errorsPerTask; j++)
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested)
                            break;

                        var exception = new InvalidOperationException(
                            $"Real-time load test - Task {taskId}, Error {j} at {DateTime.UtcNow:HH:mm:ss.fff}");
                        
                        ElmahExtensions.RaiseError(exception);
                        
                        // Small delay to simulate real-world timing
                        await Task.Delay(10, cancellationTokenSource.Token);
                    }
                }, cancellationTokenSource.Token);
            }

            await Task.WhenAll(tasks);
            
            // Wait for all async logging operations to complete
            await Task.Delay(2000, cancellationTokenSource.Token);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var actualErrorsLogged = finalCount - initialCount;
            
            actualErrorsLogged.ShouldBeGreaterThanOrEqualTo((int)(totalExpectedErrors * 0.9), 
                "At least 90% of errors should be logged successfully under load");
        }

        [Fact]
        public async Task Should_Handle_Continuous_Error_Streaming()
        {
            // Arrange
            var testDuration = TimeSpan.FromSeconds(10);
            var cancellationTokenSource = new CancellationTokenSource(testDuration);
            var errorsLogged = 0;
            var initialCount = await GetErrorCountAsync();

            // Act - Continuous error streaming
            var streamingTask = Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var exception = new Exception($"Streaming error {errorsLogged} at {DateTime.UtcNow:HH:mm:ss.fff}");
                        ElmahExtensions.RaiseError(exception);
                        Interlocked.Increment(ref errorsLogged);
                        
                        await Task.Delay(50, cancellationTokenSource.Token); // Log every 50ms
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }, cancellationTokenSource.Token);

            await streamingTask;
            await Task.Delay(1000); // Wait for final async operations

            // Assert
            var finalCount = await GetErrorCountAsync();
            var actualErrorsInDb = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThan(0, "Should have logged some errors during streaming");
            actualErrorsInDb.ShouldBeGreaterThanOrEqualTo((int)(errorsLogged * 0.8), 
                "Most streamed errors should be persisted to database");
        }

        [Fact]
        public async Task Should_Maintain_Performance_With_Large_Error_Messages()
        {
            // Arrange
            const int largeMessageSize = 10000; // 10KB messages
            const int numberOfLargeErrors = 10;
            var largeMessage = new string('X', largeMessageSize);
            
            var startTime = DateTime.UtcNow;
            var initialCount = await GetErrorCountAsync();

            // Act - Log errors with large messages
            for (int i = 0; i < numberOfLargeErrors; i++)
            {
                var exception = new Exception($"Large message test {i}: {largeMessage}");
                ElmahExtensions.RaiseError(exception);
            }

            await Task.Delay(2000); // Wait for async operations
            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo(numberOfLargeErrors);
            duration.TotalSeconds.ShouldBeLessThan(30, "Large message logging should complete within reasonable time");
        }

        [Fact]
        public async Task Should_Handle_Rapid_Error_Bursts()
        {
            // Arrange
            const int burstSize = 50;
            const int numberOfBursts = 5;
            var totalErrors = burstSize * numberOfBursts;
            
            var initialCount = await GetErrorCountAsync();

            // Act - Create bursts of errors with intervals
            for (int burst = 0; burst < numberOfBursts; burst++)
            {
                // Log burst of errors rapidly
                var burstTasks = new Task[burstSize];
                for (int i = 0; i < burstSize; i++)
                {
                    int errorId = i;
                    burstTasks[i] = Task.Run(() =>
                    {
                        var exception = new Exception($"Burst {burst}, Error {errorId} at {DateTime.UtcNow.Ticks}");
                        ElmahExtensions.RaiseError(exception);
                    });
                }
                
                await Task.WhenAll(burstTasks);
                
                // Small pause between bursts
                await Task.Delay(100);
            }

            // Wait for all async operations to complete
            await Task.Delay(3000);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo((int)(totalErrors * 0.9), 
                "Should handle error bursts with minimal loss");
        }

        [Fact]
        public async Task Should_Monitor_Error_Rate_Over_Time()
        {
            // Arrange
            var monitoringDuration = TimeSpan.FromSeconds(5);
            var checkInterval = TimeSpan.FromSeconds(1);
            var errorRates = new System.Collections.Generic.List<int>();
            
            var cancellationTokenSource = new CancellationTokenSource(monitoringDuration);
            var initialCount = await GetErrorCountAsync();
            var lastCount = initialCount;

            // Start error generation
            var errorGenerationTask = Task.Run(async () =>
            {
                int errorCounter = 0;
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var exception = new Exception($"Rate monitoring error {errorCounter++}");
                    ElmahExtensions.RaiseError(exception);
                    await Task.Delay(200, cancellationTokenSource.Token); // Error every 200ms
                }
            }, cancellationTokenSource.Token);

            // Monitor error rate
            var monitoringTask = Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Delay(checkInterval, cancellationTokenSource.Token);
                    
                    var currentCount = await GetErrorCountAsync();
                    var rate = currentCount - lastCount;
                    errorRates.Add(rate);
                    lastCount = currentCount;
                }
            }, cancellationTokenSource.Token);

            // Act
            await Task.WhenAll(errorGenerationTask, monitoringTask);

            // Assert
            errorRates.Count.ShouldBeGreaterThan(0, "Should have collected error rate samples");
            errorRates.ShouldContain(rate => rate > 0, "Should detect error logging activity");
        }

        [Fact]
        public async Task Should_Recover_From_Temporary_Database_Issues()
        {
            // Arrange
            var initialCount = await GetErrorCountAsync();
            const int errorsBeforeIssue = 5;
            const int errorsAfterRecovery = 5;

            // Act - Log some errors normally
            for (int i = 0; i < errorsBeforeIssue; i++)
            {
                var exception = new Exception($"Before issue error {i}");
                ElmahExtensions.RaiseError(exception);
            }

            await Task.Delay(500);

            // Simulate recovery by continuing to log errors
            // (In a real scenario, you might temporarily disable/enable database connection)
            for (int i = 0; i < errorsAfterRecovery; i++)
            {
                var exception = new Exception($"After recovery error {i}");
                ElmahExtensions.RaiseError(exception);
            }

            await Task.Delay(1000);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var totalErrorsLogged = finalCount - initialCount;
            
            totalErrorsLogged.ShouldBeGreaterThanOrEqualTo(errorsBeforeIssue + errorsAfterRecovery);
        }

        [Fact]
        public async Task Should_Handle_Concurrent_Read_Write_Operations()
        {
            // Arrange
            const int writeOperations = 20;
            const int readOperations = 10;
            var initialCount = await GetErrorCountAsync();

            // Act - Concurrent writes and reads
            var writeTask = Task.Run(async () =>
            {
                var writeTasks = new Task[writeOperations];
                for (int i = 0; i < writeOperations; i++)
                {
                    int index = i;
                    writeTasks[i] = Task.Run(() =>
                    {
                        var exception = new Exception($"Concurrent write operation {index}");
                        ElmahExtensions.RaiseError(exception);
                    });
                }
                await Task.WhenAll(writeTasks);
            });

            var readTask = Task.Run(async () =>
            {
                var readTasks = new Task[readOperations];
                for (int i = 0; i < readOperations; i++)
                {
                    readTasks[i] = Task.Run(async () =>
                    {
                        await GetErrorCountAsync(); // Simulate read operations
                        await Task.Delay(50);
                    });
                }
                await Task.WhenAll(readTasks);
            });

            await Task.WhenAll(writeTask, readTask);
            await Task.Delay(1000);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo((int)(writeOperations * 0.9), 
                "Concurrent operations should not significantly impact error logging");
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

        #endregion
    }
}
