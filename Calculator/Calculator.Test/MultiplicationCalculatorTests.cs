using System;
using Xunit;

namespace Calculator.Tests
{
    public class MultiplicationCalculatorTests
    {
        [Theory]
        [InlineData("123456789", "1", "123456789")]
        [InlineData("9999999", "0", "0")]
        [InlineData("0", "9999999", "0")]
        [InlineData("555555555", "2", "1111111110")]
        [InlineData("999999", "999999", "999998000001")]
        [InlineData("212121", "171717", "36424781757")]
        public void CalculateResult_PositiveNumbers(string firstNumber, string secondNumber, string expectedResult)
        {
            // Assert
            MultiplicationCalculator calculator = new MultiplicationCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("123456789", "-1", "-123456789")]
        [InlineData("-9999999", "0", "0")]
        [InlineData("0", "-9999999", "0")]
        [InlineData("-555555555", "2", "-1111111110")]
        [InlineData("999999", "-999999", "-999998000001")]
        [InlineData("-212121", "-171717", "36424781757")]
        public void CalculateResult_NegativeNumbers(string firstNumber, string secondNumber, string expectedResult)
        {
            // Assert
            MultiplicationCalculator calculator = new MultiplicationCalculator();

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
            MultiplicationCalculator calculator = new MultiplicationCalculator();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(firstNumber, secondNumber));
        }
    }
}
