using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpressionParser calculator = new ExpressionParser();
            string expression = Console.ReadLine();
            try
            {
                Console.WriteLine(calculator.Parse(expression));
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divide by zero");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
