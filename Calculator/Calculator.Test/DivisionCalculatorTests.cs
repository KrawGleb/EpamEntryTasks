using System;
using Xunit;

namespace Calculator.Tests
{
    public class DivisionCalculatorTests
    {
        [Theory]
        [InlineData("0", "1342305823049", "0")]
        [InlineData("123456789", "123", "1003713")]
        [InlineData("234322134", "235234", "996")]
        [InlineData("45234123348", "34634", "1306061")]
        [InlineData("3423480520348023", "232356543", "14733738")]
        public void Calculate_PositiveNumbers_ToInt(string firstNumber, string secondNumber, string expected)
        {
            // Arrange
            DivisionCalculator calculator = new DivisionCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);
            string wholePart = result.Split(".")[0];

            // Assert
            Assert.Equal(expected, wholePart);
        }

        [Theory]
        [InlineData("qwerty", "123")]
        [InlineData("", "")]
        public void InvalidData(string firstNumber, string secondNumber)
        {
            // Arrange
            DivisionCalculator calculator = new DivisionCalculator();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(firstNumber, secondNumber));
        }

        [Fact]
        public void Calculate_DivideByZero()
        {
            // Arrange
            DivisionCalculator calculator = new DivisionCalculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Calculate("123", "0"));
        }
    }
}
