using Morpho.Application.Commands.Users;
using Morpho.Domain.ValueObjects;
using Shouldly;
using Xunit;
using Morpho.Users.Validators;

namespace Morpho.Tests.Application.Validators
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;

        public CreateUserCommandValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Fact]
        public void Validate_WithValidCommand_ShouldPass()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "John",
                Surname = "Doe",
                UserName = "john.doe",
                EmailAddress = new Email("john.doe@example.com"),
                Password = "Password123",
                RoleNames = ["User"]
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_WithInvalidName_ShouldFail(string name)
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = name,
                Surname = "Doe",
                UserName = "john.doe",
                EmailAddress = new Email("john.doe@example.com"),
                Password = "Password123"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == nameof(CreateUserCommand.Name));
        }

        [Fact]
        public void Validate_WithTooLongName_ShouldFail()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = new string('a', 65), // 65 characters, exceeds limit of 64
                Surname = "Doe",
                UserName = "john.doe",
                EmailAddress = new Email("john.doe@example.com"),
                Password = "Password123"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == nameof(CreateUserCommand.Name));
        }

        [Theory]
        [InlineData("user@name")]
        [InlineData("user name")]
        [InlineData("user#name")]
        public void Validate_WithInvalidUserName_ShouldFail(string userName)
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "John",
                Surname = "Doe",
                UserName = userName,
                EmailAddress = new Email("john.doe@example.com"),
                Password = "Password123"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == nameof(CreateUserCommand.UserName));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("password")]
        [InlineData("PASSWORD")]
        [InlineData("Password")]
        [InlineData("12345")]
        public void Validate_WithWeakPassword_ShouldFail(string password)
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "John",
                Surname = "Doe",
                UserName = "john.doe",
                EmailAddress = new Email("john.doe@example.com"),
                Password = password
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == nameof(CreateUserCommand.Password));
        }

        [Fact]
        public void Validate_WithNullEmail_ShouldFail()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "John",
                Surname = "Doe",
                UserName = "john.doe",
                EmailAddress = null,
                Password = "Password123"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == nameof(CreateUserCommand.EmailAddress));
        }
    }
}