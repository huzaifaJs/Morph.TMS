using System;
using Morpho.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Constructor_WithValidEmail_ShouldCreateInstance()
        {
            // Arrange
            var emailValue = "test@example.com";

            // Act
            var email = new Email(emailValue);

            // Assert
            email.Value.ShouldBe(emailValue);
        }

        [Fact]
        public void Constructor_WithValidEmailUpperCase_ShouldNormalizeToLowerCase()
        {
            // Arrange
            var emailValue = "TEST@EXAMPLE.COM";

            // Act
            var email = new Email(emailValue);

            // Assert
            email.Value.ShouldBe("test@example.com");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_WithInvalidEmail_ShouldThrowException(string emailValue)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Email(emailValue));
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("invalid@")]
        [InlineData("@example.com")]
        [InlineData("invalid.example.com")]
        public void Constructor_WithMalformedEmail_ShouldThrowException(string emailValue)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Email(emailValue));
        }

        [Fact]
        public void Equality_WithSameEmail_ShouldBeEqual()
        {
            // Arrange
            var email1 = new Email("test@example.com");
            var email2 = new Email("test@example.com");

            // Act & Assert
            email1.ShouldBe(email2);
            (email1 == email2).ShouldBeTrue();
            (email1 != email2).ShouldBeFalse();
        }

        [Fact]
        public void Equality_WithDifferentEmails_ShouldNotBeEqual()
        {
            // Arrange
            var email1 = new Email("test1@example.com");
            var email2 = new Email("test2@example.com");

            // Act & Assert
            email1.ShouldNotBe(email2);
            (email1 == email2).ShouldBeFalse();
            (email1 != email2).ShouldBeTrue();
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldWork()
        {
            // Arrange
            var email = new Email("test@example.com");

            // Act
            string emailString = email;

            // Assert
            emailString.ShouldBe("test@example.com");
        }

        [Fact]
        public void ImplicitConversion_FromString_ShouldWork()
        {
            // Arrange & Act
            Email email = "test@example.com";

            // Assert
            email.Value.ShouldBe("test@example.com");
        }
    }
}