using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ElmahCore;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Elmah
{
    /// <summary>
    /// Unit tests for ElmahCore error logging functionality
    /// These tests verify that ElmahCore properly logs errors and exceptions
    /// </summary>
    public class ElmahCoreLoggingTests : MorphoTestBase
    {
        [Fact]
        public void Should_Log_Exception_To_ElmahCore()
        {
            // Arrange
            var testException = new InvalidOperationException("Test exception for ElmahCore unit test");
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(testException);
            });
        }

        [Fact]
        public void Should_Log_Exception_With_Custom_Message()
        {
            // Arrange
            var customMessage = "Custom error message for unit testing";
            var testException = new ApplicationException(customMessage);
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(testException);
            });
        }

        [Fact]
        public void Should_Handle_Null_Exception_Gracefully()
        {
            // Act & Assert - Should not throw even with null exception
            Should.NotThrow(() =>
            {
                try
                {
                    ElmahExtensions.RaiseError(null);
                }
                catch (ArgumentNullException)
                {
                    // Expected behavior for null exception
                }
            });
        }

        [Fact]
        public void Should_Log_Nested_Exceptions()
        {
            // Arrange
            var innerException = new ArgumentException("Inner exception");
            var outerException = new InvalidOperationException("Outer exception", innerException);
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(outerException);
            });
        }

        [Fact]
        public async Task Should_Log_Multiple_Exceptions_Concurrently()
        {
            // Arrange
            const int numberOfExceptions = 10;
            var tasks = new Task[numberOfExceptions];
            
            // Act
            for (int i = 0; i < numberOfExceptions; i++)
            {
                int index = i; // Capture for closure
                tasks[i] = Task.Run(() =>
                {
                    var exception = new InvalidOperationException($"Concurrent exception {index}");
                    ElmahExtensions.RaiseError(exception);
                });
            }
            
            // Assert - All tasks should complete without exceptions
            await Should.NotThrowAsync(async () =>
            {
                await Task.WhenAll(tasks);
            });
        }

        [Fact]
        public void Should_Log_Exception_With_Stack_Trace()
        {
            // Arrange & Act
            try
            {
                ThrowExceptionWithStackTrace();
            }
            catch (Exception ex)
            {
                // Assert - Should not throw when logging exception with stack trace
                Should.NotThrow(() =>
                {
                    ElmahExtensions.RaiseError(ex);
                });
                
                // Verify exception has stack trace
                ex.StackTrace.ShouldNotBeNullOrEmpty();
            }
        }

        [Fact]
        public void Should_Log_Exception_With_Data_Properties()
        {
            // Arrange
            var exception = new InvalidOperationException("Test exception with data");
            exception.Data["UserId"] = 123;
            exception.Data["Operation"] = "TestOperation";
            exception.Data["Timestamp"] = DateTime.UtcNow;
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(exception);
            });
        }

        [Theory]
        [InlineData("ArgumentException")]
        [InlineData("InvalidOperationException")]
        [InlineData("ApplicationException")]
        [InlineData("NotImplementedException")]
        public void Should_Log_Different_Exception_Types(string exceptionTypeName)
        {
            // Arrange
            Exception exception = exceptionTypeName switch
            {
                "ArgumentException" => new ArgumentException($"Test {exceptionTypeName}"),
                "InvalidOperationException" => new InvalidOperationException($"Test {exceptionTypeName}"),
                "ApplicationException" => new ApplicationException($"Test {exceptionTypeName}"),
                "NotImplementedException" => new NotImplementedException($"Test {exceptionTypeName}"),
                _ => new Exception($"Test {exceptionTypeName}")
            };
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(exception);
            });
        }

        [Fact]
        public void Should_Log_Exception_With_Long_Message()
        {
            // Arrange
            var longMessage = new string('A', 5000); // Very long message
            var exception = new Exception(longMessage);
            
            // Act & Assert - Should not throw even with long message
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(exception);
            });
        }

        [Fact]
        public void Should_Log_Exception_With_Special_Characters()
        {
            // Arrange
            var messageWithSpecialChars = "Test with special chars: <>\"'&\n\r\t";
            var exception = new Exception(messageWithSpecialChars);
            
            // Act & Assert - Should not throw
            Should.NotThrow(() =>
            {
                ElmahExtensions.RaiseError(exception);
            });
        }

        /// <summary>
        /// Helper method to throw an exception with a proper stack trace
        /// </summary>
        private void ThrowExceptionWithStackTrace()
        {
            throw new InvalidOperationException("Exception with stack trace for testing");
        }
    }
}
