using System;

namespace Calculator
{
    public class MultiplicationCalculator : ICalculator
    {
        readonly SumCalculator _sumCalculator;

        public MultiplicationCalculator()
        {
            _sumCalculator = new SumCalculator();
        }

        public string Calculate(string firstElement, string secondElement)
        {
            if (!(firstElement.IsNumber() && secondElement.IsNumber()))
            {
                throw new ArgumentException("Argumnets are not numbers");
            }

            if (secondElement == "0" || firstElement == "0")
            {
                return "0";
            }

            if (firstElement.IsNegativeNumber() || secondElement.IsNegativeNumber())
            {
                return MultiplyNegativeNumbers(firstElement, secondElement);
            }

            (firstElement, secondElement) = firstElement.Length > secondElement.Length ? (firstElement, secondElement) : (secondElement, firstElement);
            string output = "0";

            for (int digit = 1; digit <= secondElement.Length; digit++)
            {
                char nextMultiplier = secondElement[^digit];

                string multiplicationResult = CalculateMultiplicationByDigit(firstElement, nextMultiplier);
                multiplicationResult = multiplicationResult.PadRight(multiplicationResult.Length + digit - 1, '0');

                output = _sumCalculator.Calculate(output, multiplicationResult);
            }

            return output;
        }

        private string CalculateMultiplicationByDigit(string number, char digit)
        {
            string output = number;
            bool canParseDigit = int.TryParse(digit.ToString(), out int digitNumericStyle);

            if (!canParseDigit)
            {
                throw new ArgumentException(nameof(digit));
            }
            else
            {
                for (int _ = 0; _ < digitNumericStyle - 1; _++)
                {
                    output = _sumCalculator.Calculate(output, number);
                }
            }

            return output;
        }

        private string MultiplyNegativeNumbers(string firstElement, string secondElement)
        {
            if (firstElement.IsNegativeNumber() && secondElement.IsNegativeNumber())
            {
                return Calculate(firstElement.Trim('-'), secondElement.Trim('-'));
            }
            else if (firstElement.IsNegativeNumber() || secondElement.IsNegativeNumber())
            {
                return "-" + Calculate(firstElement.Trim('-'), secondElement.Trim('-'));
            }

            throw new ArgumentException("Numbers are positive");
        }
    }
}
