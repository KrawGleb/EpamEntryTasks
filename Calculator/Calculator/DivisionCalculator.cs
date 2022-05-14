using System;
using System.Text;

namespace Calculator
{
    public class DivisionCalculator : ICalculator
    {
        readonly SubtractionCalculator _subtractionCalculator;

        public DivisionCalculator()
        {
            _subtractionCalculator = new SubtractionCalculator();
        }

        public string Calculate(string firstElement, string secondElement)
        {
            if (!(firstElement.IsNumber() && secondElement.IsNumber()))
            {
                throw new ArgumentException("Argumnets are not numbers");
            }

            if (string.IsNullOrEmpty(secondElement.Trim('0')))
            {
                throw new DivideByZeroException();
            }

            if (string.IsNullOrEmpty(firstElement.Trim('0')))
            {
                return "0";
            }

            if (firstElement.IsNegativeNumber() || secondElement.IsNegativeNumber())
            {
                return DivideNegativeNumbers(firstElement, secondElement);
            }

            const int accuracy = 6;
            firstElement = firstElement.PadRight(firstElement.Length + accuracy, '0');

            StringBuilder output = new StringBuilder();
            string remainder = firstElement[0..secondElement.Length];

            for (int digit = secondElement.Length; digit <= firstElement.Length; digit++)
            {
                int counter = 0;
                string prevRemainder = remainder;

                while (string.IsNullOrEmpty(remainder) || !remainder.IsNegativeNumber())
                {
                    prevRemainder = remainder;
                    remainder = _subtractionCalculator.Calculate(remainder, secondElement);
                    counter++;
                }

                remainder = digit != firstElement.Length ? prevRemainder + firstElement[digit] : prevRemainder;
                counter--;

                output.Append(counter.ToString());
            }

            string outputStringStyle = output.ToString();
            output.Insert(outputStringStyle.Length - accuracy, '.');

            return output.ToString().TrimStart('0');
        }

        private string DivideNegativeNumbers(string firstElement, string secondElement)
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
