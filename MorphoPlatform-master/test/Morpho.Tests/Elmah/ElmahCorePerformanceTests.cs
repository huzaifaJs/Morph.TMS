using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Elmah
{
    /// <summary>
    /// Performance and stress tests for ElmahCore
    /// These tests verify that ElmahCore maintains good performance under various conditions
    /// </summary>
    public class ElmahCorePerformanceTests : MorphoTestBase
    {
        private readonly string _connectionString;

        public ElmahCorePerformanceTests()
        {
            _connectionString = LocalIocManager.Resolve<IConfiguration>()
                .GetConnectionString("Default");
        }

        [Fact]
        public async Task Should_Log_1000_Errors_Within_Reasonable_Time()
        {
            // Arrange
            const int numberOfErrors = 1000;
            var stopwatch = Stopwatch.StartNew();
            var initialCount = await GetErrorCountAsync();

            // Act
            var tasks = new Task[numberOfErrors];
            for (int i = 0; i < numberOfErrors; i++)
            {
                int index = i;
                tasks[i] = Task.Run(() =>
                {
                    var exception = new Exception($"Performance test error {index}");
                    ElmahExtensions.RaiseError(exception);
                });
            }

            await Task.WhenAll(tasks);
            await Task.Delay(5000); // Wait for async operations
            stopwatch.Stop();

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo((int)(numberOfErrors * 0.95), 
                "Should log at least 95% of errors");
            
            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(60000, 
                "Should complete 1000 error logging within 60 seconds");
            
            // Calculate performance metrics
            var errorsPerSecond = errorsLogged / stopwatch.Elapsed.TotalSeconds;
            errorsPerSecond.ShouldBeGreaterThan(10, "Should maintain reasonable throughput");
        }

        [Fact]
        public async Task Should_Handle_Memory_Pressure_During_Error_Logging()
        {
            // Arrange
            const int numberOfErrors = 100;
            const int largeObjectSize = 1024 * 1024; // 1MB objects
            var initialCount = await GetErrorCountAsync();

            // Act - Create memory pressure while logging errors
            var memoryPressureTask = Task.Run(() =>
            {
                var largeObjects = new byte[10][]; // 10 MB total
                for (int i = 0; i < 10; i++)
                {
                    largeObjects[i] = new byte[largeObjectSize];
                }
                
                // Keep objects in memory during test
                System.Threading.Thread.Sleep(5000);
                
                // Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            });

            var errorLoggingTask = Task.Run(async () =>
            {
                for (int i = 0; i < numberOfErrors; i++)
                {
                    var exception = new Exception($"Memory pressure test error {i}");
                    ElmahExtensions.RaiseError(exception);
                    await Task.Delay(10); // Small delay
                }
            });

            await Task.WhenAll(memoryPressureTask, errorLoggingTask);
            await Task.Delay(2000);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo((int)(numberOfErrors * 0.9), 
                "Should maintain error logging capability under memory pressure");
        }

        [Fact]
        public async Task Should_Maintain_Performance_With_Complex_Exception_Objects()
        {
            // Arrange
            const int numberOfComplexErrors = 50;
            var stopwatch = Stopwatch.StartNew();
            var initialCount = await GetErrorCountAsync();

            // Act - Log complex exceptions with nested data
            for (int i = 0; i < numberOfComplexErrors; i++)
            {
                var complexException = CreateComplexException(i);
                ElmahExtensions.RaiseError(complexException);
            }

            await Task.Delay(3000); // Wait for async operations
            stopwatch.Stop();

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo(numberOfComplexErrors);
            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(30000, 
                "Complex exception logging should complete within 30 seconds");
        }

        [Fact]
        public async Task Should_Handle_Database_Connection_Pool_Exhaustion()
        {
            // Arrange
            const int simultaneousConnections = 50;
            var initialCount = await GetErrorCountAsync();

            // Act - Create many simultaneous database operations
            var connectionTasks = new Task[simultaneousConnections];
            for (int i = 0; i < simultaneousConnections; i++)
            {
                int index = i;
                connectionTasks[i] = Task.Run(async () =>
                {
                    // Log error (which uses database)
                    var exception = new Exception($"Connection pool test {index}");
                    ElmahExtensions.RaiseError(exception);
                    
                    // Also make direct database query
                    await GetErrorCountAsync();
                });
            }

            await Task.WhenAll(connectionTasks);
            await Task.Delay(2000);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo((int)(simultaneousConnections * 0.8), 
                "Should handle connection pool pressure gracefully");
        }

        [Fact]
        public async Task Should_Perform_Well_With_Long_Running_Operations()
        {
            // Arrange
            const int operationDurationMinutes = 1; // 1 minute test
            const int errorInterval = 1000; // Log error every second
            
            var testDuration = TimeSpan.FromMinutes(operationDurationMinutes);
            var cancellationTokenSource = new System.Threading.CancellationTokenSource(testDuration);
            var initialCount = await GetErrorCountAsync();
            var errorsLogged = 0;

            // Act - Long running error logging
            var longRunningTask = Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var exception = new Exception($"Long running test error {errorsLogged} at {DateTime.UtcNow}");
                        ElmahExtensions.RaiseError(exception);
                        errorsLogged++;
                        
                        await Task.Delay(errorInterval, cancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }, cancellationTokenSource.Token);

            await longRunningTask;
            await Task.Delay(2000); // Wait for final operations

            // Assert
            var finalCount = await GetErrorCountAsync();
            var actualErrorsInDb = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThan(0, "Should have logged errors during long running operation");
            actualErrorsInDb.ShouldBeGreaterThanOrEqualTo((int)(errorsLogged * 0.9), 
                "Should maintain consistency in long running scenarios");
        }

        [Fact]
        public async Task Should_Maintain_Consistent_Performance_Over_Multiple_Batches()
        {
            // Arrange
            const int numberOfBatches = 5;
            const int errorsPerBatch = 20;
            var batchTimes = new double[numberOfBatches];
            var initialCount = await GetErrorCountAsync();

            // Act - Measure performance across multiple batches
            for (int batch = 0; batch < numberOfBatches; batch++)
            {
                var batchStopwatch = Stopwatch.StartNew();
                
                var batchTasks = new Task[errorsPerBatch];
                for (int i = 0; i < errorsPerBatch; i++)
                {
                    int errorIndex = i;
                    batchTasks[i] = Task.Run(() =>
                    {
                        var exception = new Exception($"Batch {batch}, Error {errorIndex}");
                        ElmahExtensions.RaiseError(exception);
                    });
                }
                
                await Task.WhenAll(batchTasks);
                batchStopwatch.Stop();
                batchTimes[batch] = batchStopwatch.ElapsedMilliseconds;
                
                // Small delay between batches
                await Task.Delay(500);
            }

            await Task.Delay(3000); // Final wait

            // Assert
            var finalCount = await GetErrorCountAsync();
            var totalErrorsLogged = finalCount - initialCount;
            
            totalErrorsLogged.ShouldBeGreaterThanOrEqualTo((int)(numberOfBatches * errorsPerBatch * 0.9));
            
            // Check performance consistency
            var averageTime = batchTimes.Average();
            var maxTime = batchTimes.Max();
            var minTime = batchTimes.Min();
            
            // Performance should not degrade significantly across batches
            (maxTime - minTime).ShouldBeLessThan(averageTime * 2, 
                "Performance should remain relatively consistent across batches");
        }

        [Fact]
        public async Task Should_Handle_Resource_Cleanup_Properly()
        {
            // Arrange
            const int numberOfOperations = 100;
            var initialCount = await GetErrorCountAsync();
            
            // Get initial memory usage
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            var initialMemory = GC.GetTotalMemory(false);

            // Act - Perform many operations that should be cleaned up
            for (int i = 0; i < numberOfOperations; i++)
            {
                var exception = new Exception($"Resource cleanup test {i}");
                ElmahExtensions.RaiseError(exception);
                
                // Force some allocations
                var tempData = new byte[1024];
                tempData[0] = (byte)i;
            }

            await Task.Delay(2000);

            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            var finalMemory = GC.GetTotalMemory(false);

            // Assert
            var finalCount = await GetErrorCountAsync();
            var errorsLogged = finalCount - initialCount;
            
            errorsLogged.ShouldBeGreaterThanOrEqualTo(numberOfOperations);
            
            // Memory usage should not have grown excessively
            var memoryIncrease = finalMemory - initialMemory;
            memoryIncrease.ShouldBeLessThan(10 * 1024 * 1024, // 10MB threshold
                "Memory usage should not increase excessively after operations");
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

        private Exception CreateComplexException(int index)
        {
            var innerException = new ArgumentException($"Inner exception {index}", "parameter");
            innerException.Data["InnerIndex"] = index;
            innerException.Data["InnerTimestamp"] = DateTime.UtcNow;
            
            var middleException = new InvalidOperationException($"Middle exception {index}", innerException);
            middleException.Data["MiddleIndex"] = index;
            middleException.Data["MiddleData"] = new { Id = index, Name = $"Test{index}" };
            
            var outerException = new ApplicationException($"Outer exception {index}", middleException);
            outerException.Data["OuterIndex"] = index;
            outerException.Data["ComplexData"] = new 
            { 
                Timestamp = DateTime.UtcNow,
                Details = "Complex exception for performance testing",
                Metadata = new { Version = "1.0", Environment = "Test" }
            };
            
            return outerException;
        }

        #endregion
    }
}
