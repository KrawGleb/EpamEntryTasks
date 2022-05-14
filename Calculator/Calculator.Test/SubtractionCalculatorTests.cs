using System;
using Xunit;

namespace Calculator.Tests
{
    public class SubtractionCalculatorTests
    {
        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "1", "0")]
        [InlineData("89734578935477", "0", "89734578935477")]
        [InlineData("0", "89734578935477", "-89734578935477")]
        [InlineData("6545639572340827", "769247592", "6545638803093235")]
        public void Calculate_PositiveNumbers(string firstNumber, string secondNumber, string expected)
        {
            // Arrange
            SubtractionCalculator calculator = new SubtractionCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-1", "-1", "0")]
        [InlineData("-1", "1", "-2")]
        [InlineData("-87421042", "277963", "-87699005")]
        [InlineData("-143254332341244", "-12353434124", "-143241978907120")]
        public void Calculate_NegativeNumbers(string firstNumber, string secondNumber, string expected)
        {
            // Arrange
            SubtractionCalculator calculator = new SubtractionCalculator();

            // Act
            string result = calculator.Calculate(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expected, result);
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
