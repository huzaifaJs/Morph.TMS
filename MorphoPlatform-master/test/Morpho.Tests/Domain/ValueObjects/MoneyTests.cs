using System;
using Morpho.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Morpho.Tests.Domain.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void Constructor_WithValidAmountAndCurrency_ShouldCreateInstance()
        {
            // Arrange & Act
            var money = new Money(100.50m, "USD");

            // Assert
            money.Amount.ShouldBe(100.50m);
            money.Currency.ShouldBe("USD");
        }

        [Fact]
        public void Constructor_WithNegativeAmount_ShouldThrowException()
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Money(-100, "USD"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("US")]
        [InlineData("USDD")]
        public void Constructor_WithInvalidCurrency_ShouldThrowException(string currency)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Money(100, currency));
        }

        [Fact]
        public void Constructor_WithLowerCaseCurrency_ShouldNormalizeToUpperCase()
        {
            // Arrange & Act
            var money = new Money(100, "usd");

            // Assert
            money.Currency.ShouldBe("USD");
        }

        [Fact]
        public void Add_WithSameCurrency_ShouldReturnCorrectSum()
        {
            // Arrange
            var money1 = new Money(100, "USD");
            var money2 = new Money(50, "USD");

            // Act
            var result = money1.Add(money2);

            // Assert
            result.Amount.ShouldBe(150);
            result.Currency.ShouldBe("USD");
        }

        [Fact]
        public void Add_WithDifferentCurrencies_ShouldThrowException()
        {
            // Arrange
            var money1 = new Money(100, "USD");
            var money2 = new Money(50, "EUR");

            // Act & Assert
            Should.Throw<InvalidOperationException>(() => money1.Add(money2));
        }

        [Fact]
        public void Subtract_WithSameCurrency_ShouldReturnCorrectDifference()
        {
            // Arrange
            var money1 = new Money(100, "USD");
            var money2 = new Money(30, "USD");

            // Act
            var result = money1.Subtract(money2);

            // Assert
            result.Amount.ShouldBe(70);
            result.Currency.ShouldBe("USD");
        }

        [Fact]
        public void Multiply_ShouldReturnCorrectProduct()
        {
            // Arrange
            var money = new Money(100, "USD");

            // Act
            var result = money.Multiply(1.5m);

            // Assert
            result.Amount.ShouldBe(150);
            result.Currency.ShouldBe("USD");
        }

        [Fact]
        public void Operators_ShouldWorkCorrectly()
        {
            // Arrange
            var money1 = new Money(100, "USD");
            var money2 = new Money(50, "USD");

            // Act & Assert
            (money1 + money2).Amount.ShouldBe(150);
            (money1 - money2).Amount.ShouldBe(50);
            (money1 * 2).Amount.ShouldBe(200);
        }

        [Fact]
        public void Equality_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var money1 = new Money(100, "USD");
            var money2 = new Money(100, "USD");

            // Act & Assert
            money1.ShouldBe(money2);
            (money1 == money2).ShouldBeTrue();
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var money = new Money(100.50m, "USD");

            // Act
            var result = money.ToString();

            // Assert
            result.ShouldBe("100.50 USD");
        }
    }
}