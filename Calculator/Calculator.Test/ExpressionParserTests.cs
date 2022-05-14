using System;
using Xunit;

namespace Calculator.Tests
{
    public class ExpressionParserTests
    {
        [Theory]
        [InlineData("111111111+111111111", "222222222")]
        [InlineData("53462342634-12859034", "53449483600")]
        [InlineData("999999*-999999", "-999998000001")]
        [InlineData("23432434523421+-8789645623", "23423644877798")]
        [InlineData("-14335345123436754353--546234786786545", "-14334798888649967808")]
        [InlineData("-546234786786545--14335345123436754353", "14334798888649967808")]
        public void Parse_ValidExpression(string expression, string expected)
        {
            // Arrange
            ExpressionParser expressionParser = new ExpressionParser();

            // Act
            string result = expressionParser.Parse(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("/")]
        [InlineData("werw+gerwef")]
        [InlineData("12342343%42342")]
        public void Parse_InvalidExpression(string expression)
        {
            // Arrange
            ExpressionParser expressionParser = new ExpressionParser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => expressionParser.Parse(expression));
        }
    }
}
