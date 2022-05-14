using System;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class SumCalculator : ICalculator
    {
        public string Calculate(string firstElement, string secondElement)
        {
            if (!(firstElement.IsNumber() && secondElement.IsNumber()))
            {
                throw new ArgumentException("Argumnets are not numbers");
            }

            if (firstElement.IsNegativeNumber() || secondElement.IsNegativeNumber())
            {
                return AddNegativeNumbers(firstElement, secondElement);
            }

            if (firstElement.Length != secondElement.Length)
            {
                (firstElement, secondElement) = firstElement.Length > secondElement.Length ? (firstElement, secondElement) : (secondElement, firstElement);

                secondElement = secondElement.PadLeft(firstElement.Length, '0');
            }

            StringBuilder output = new StringBuilder();
            int remainder = 0;

            for (int digit = 1; digit <= firstElement.Length; digit++)
            {
                bool canParseFirstDigit = int.TryParse(firstElement[^digit].ToString(), out int firstDigit);
                bool canParseSecondDigit = int.TryParse(secondElement[^digit].ToString(), out int secondDigit);

                if (!(canParseFirstDigit && canParseSecondDigit))
                {
                    throw new ArgumentException("Invalid elements");
                }

                int result = firstDigit + secondDigit + remainder;
                remainder = result / 10;
                result %= 10;

                output.Append(result.ToString());

            }

            if (remainder != 0)
            {
                output.Append(remainder.ToString());
            }

            return new string(output.ToString().Reverse().ToArray());
        }

        private string AddNegativeNumbers(string firstElement, string secondElement)
        {
            if (firstElement.IsNegativeNumber() && secondElement.IsNegativeNumber())
            {
                return "-" + Calculate(firstElement[1..], secondElement[1..]);
            }
            else if (firstElement.IsNegativeNumber())
            {
                SubtractionCalculator subtractionCalculator = new SubtractionCalculator();
                return subtractionCalculator.Calculate(secondElement, firstElement[1..]);
            }
            else if (secondElement.IsNegativeNumber())
            {
                SubtractionCalculator subtractionCalculator = new SubtractionCalculator();
                return subtractionCalculator.Calculate(firstElement, secondElement[1..]);
            }

            throw new ArgumentException("Numbers are positive");
        }
    }
}
