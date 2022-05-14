using System;

namespace Calculator
{
    public class ExpressionParser
    {
        readonly char[] _operations = { '+', '/', '*', '-' };

        public string Parse(string expression)
        {
            if (expression.Contains("--"))
            {
                expression = expression.Replace("--", "+");
            }

            foreach (var operation in _operations)
            {
                if (expression.Contains(operation))
                {
                    string[] elements = expression.Split(operation);

                    if (elements.Length > 2)
                    {
                        if (string.IsNullOrEmpty(elements[0]) && operation == '-')
                        {
                            elements[1] = "-" + elements[1];
                            return Parse(elements[1..], operation);
                        }

                        throw new ArgumentException("To many elements");
                    }

                    return Parse(elements, operation);
                }
            }

            throw new ArgumentException("Invalid expression", nameof(expression));
        }

        public string Parse(string[] elements, char operation)
        {
            if (elements.Length != 2)
            {
                throw new ArgumentException("More than 2 elements", nameof(elements));
            }

            if (!(elements[0].IsNumber() && elements[1].IsNumber()))
            {
                throw new ArgumentException("Elements are not numbers", nameof(elements));
            }

            string firstElement = elements[0];
            string secondElement = elements[1];

            ICalculator calculator = operation switch
            {
                '+' => new SumCalculator(),
                '-' => new SubtractionCalculator(),
                '*' => new MultiplicationCalculator(),
                '/' => new DivisionCalculator(),
                _ => throw new ArgumentException("Invalid operation", nameof(operation)),
            };

            return calculator.Calculate(firstElement, secondElement);
        }
    }
}
