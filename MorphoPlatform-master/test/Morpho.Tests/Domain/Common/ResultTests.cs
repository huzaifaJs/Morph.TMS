using Morpho.Domain.Common;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Domain.Common
{
    public class ResultTests
    {
        [Fact]
        public void Success_ShouldCreateSuccessResult()
        {
            // Act
            var result = Result.Success();

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Error.ShouldBeNull();
        }

        [Fact]
        public void Failure_ShouldCreateFailureResult()
        {
            // Arrange
            var errorMessage = "Something went wrong";

            // Act
            var result = Result.Failure(errorMessage);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(errorMessage);
        }

        [Fact]
        public void SuccessWithValue_ShouldCreateSuccessResultWithValue()
        {
            // Arrange
            var value = "test value";

            // Act
            var result = Result.Success(value);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void FailureWithValue_ShouldCreateFailureResult()
        {
            // Arrange
            var errorMessage = "Something went wrong";

            // Act
            var result = Result.Failure<string>(errorMessage);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(errorMessage);
        }

        [Fact]
        public void AccessingValueOnFailedResult_ShouldThrowException()
        {
            // Arrange
            var result = Result.Failure<string>("Error");

            // Act & Assert
            Should.Throw<System.InvalidOperationException>(() => result.Value);
        }

        [Fact]
        public void Map_OnSuccessResult_ShouldTransformValue()
        {
            // Arrange
            var result = Result.Success(5);

            // Act
            var mappedResult = result.Map(x => x.ToString());

            // Assert
            mappedResult.IsSuccess.ShouldBeTrue();
            mappedResult.Value.ShouldBe("5");
        }

        [Fact]
        public void Map_OnFailureResult_ShouldReturnFailure()
        {
            // Arrange
            var result = Result.Failure<int>("Error");

            // Act
            var mappedResult = result.Map(x => x.ToString());

            // Assert
            mappedResult.IsFailure.ShouldBeTrue();
            mappedResult.Error.ShouldBe("Error");
        }

        [Fact]
        public void Bind_OnSuccessResult_ShouldExecuteFunction()
        {
            // Arrange
            var result = Result.Success(5);

            // Act
            var boundResult = result.Bind(x => Result.Success(x * 2));

            // Assert
            boundResult.IsSuccess.ShouldBeTrue();
            boundResult.Value.ShouldBe(10);
        }

        [Fact]
        public void Bind_OnFailureResult_ShouldReturnFailure()
        {
            // Arrange
            var result = Result.Failure<int>("Error");

            // Act
            var boundResult = result.Bind(x => Result.Success(x * 2));

            // Assert
            boundResult.IsFailure.ShouldBeTrue();
            boundResult.Error.ShouldBe("Error");
        }

        [Fact]
        public void Match_OnSuccessResult_ShouldExecuteSuccessFunction()
        {
            // Arrange
            var result = Result.Success(5);

            // Act
            var matchResult = result.Match(
                value => $"Success: {value}",
                error => $"Error: {error}"
            );

            // Assert
            matchResult.ShouldBe("Success: 5");
        }

        [Fact]
        public void Match_OnFailureResult_ShouldExecuteFailureFunction()
        {
            // Arrange
            var result = Result.Failure<int>("Something went wrong");

            // Act
            var matchResult = result.Match(
                value => $"Success: {value}",
                error => $"Error: {error}"
            );

            // Assert
            matchResult.ShouldBe("Error: Something went wrong");
        }
    }
}