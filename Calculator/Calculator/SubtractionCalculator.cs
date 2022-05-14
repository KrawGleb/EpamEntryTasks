using System;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class SubtractionCalculator : ICalculator
    {
        public string Calculate(string firstElement, string secondElement)
        {
            if (!(firstElement.IsNumber() && secondElement.IsNumber()))
            {
                throw new ArgumentException("Argumnets are not numbers");
            }

            if (firstElement.IsNegativeNumber() || secondElement.IsNegativeNumber())
            {
                return SubtractNegativeNumbers(firstElement, secondElement);
            }

            bool isPositive = true;

            if (firstElement.Length != secondElement.Length)
            {
                isPositive = firstElement.Length > secondElement.Length;

                (firstElement, secondElement) = firstElement.Length > secondElement.Length ? (firstElement, secondElement) : (secondElement, firstElement);

                secondElement = secondElement.PadLeft(firstElement.Length, '0');
            }

            StringBuilder output = new StringBuilder();
            int loan = 0;

            for (int digit = 1; digit <= firstElement.Length; digit++)
            {
                bool canParseFirstDigit = int.TryParse(firstElement[^digit].ToString(), out int firstDigit);
                bool canParseSecondDigit = int.TryParse(secondElement[^digit].ToString(), out int secondDigit);

                if (!(canParseFirstDigit && canParseSecondDigit))
                {
                    throw new ArgumentException("Invalid elements");
                }

                firstDigit -= loan;
                loan = 0;

                if (firstDigit < secondDigit)
                {
                    if (digit == firstElement.Length)
                    {
                        (firstDigit, secondDigit) = (secondDigit, firstDigit);
                        isPositive = false;
                    }
                    else
                    {
                        firstDigit += 10;
                        loan = 1;
                    }
                }

                int result = firstDigit - secondDigit;

                output.Append(result.ToString());
            }

            string resultNumber = new string(output.ToString().TrimEnd('0').Reverse().ToArray());
            resultNumber = resultNumber.Length > 0 ? resultNumber : "0";

            return isPositive ? resultNumber : '-' + resultNumber;
        }

        public string SubtractNegativeNumbers(string firstElement, string secondElement)
        {
            bool firstElementIsNegative = firstElement.IsNegativeNumber();
            bool secondElementIsNegative = secondElement.IsNegativeNumber();

            string firstElementAbs = firstElement[1..];
            string secondElementAbs = secondElement[1..];

            SumCalculator sumCalculator = new SumCalculator();

            if (firstElementIsNegative && secondElementIsNegative)
            {
                return Calculate(secondElementAbs, firstElementAbs);
            }
            else if (firstElementIsNegative)
            {
                return "-" + sumCalculator.Calculate(firstElementAbs, secondElement);
            }
            else if (secondElementIsNegative)
            {
                return sumCalculator.Calculate(firstElement, secondElementAbs);
            }

            throw new ArgumentException("Numbers are positive");
        }
    }
}
