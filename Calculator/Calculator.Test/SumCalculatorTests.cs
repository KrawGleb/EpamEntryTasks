using System;
using Xunit;

namespace Calculator.Tests
{
    public class SumCalculatorTests
    {
        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "1", "2")]
        [InlineData("123", "123", "246")]
        [InlineData("999999999999999999999999999999999999", "1", "1000000000000000000000000000000000000")]
        [InlineData("3432414235", "3424123234", "6856537469")]
        [InlineData("234975461242", "4359127432", "239334588674")]
        [InlineData("999999999", "999999999", "1999999998")]
        public void CalculateResult_PositiveNumbers(string firstNumber, string secondNumber, string expectedResult)
        {
            // Arrange
            SumCalculator calculator = new SumCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("-1", "1", "0")]
        [InlineData("-1", "-1", "-2")]
        [InlineData("-123", "0", "-123")]
        [InlineData("-0", "0", "0")]
        [InlineData("-999999999999999999999999999", "-1", "-1000000000000000000000000000")]
        public void CalculateResult_NegativeNumbers(string firstNumber, string secondNumber, string expectedResult)
        {
            // Arrange
            SumCalculator calculator = new SumCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("qwerty", "123")]
        [InlineData("", "")]
        public void InvalidData(string firstNumber, string secondNumber)
        {
            // Arrange
            SumCalculator calculator = new SumCalculator();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(firstNumber, secondNumber));
        }
    }
}
