using System.Linq;

namespace Calculator
{
    static class StringNumber
    {
        public static bool IsNumber(this string number) => number.Length > 0 && (number.All(ch => char.IsDigit(ch)) || (number[0] == '-' && number[1..].All(ch => char.IsDigit(ch))));

        public static bool IsNegativeNumber(this string number) => number.Length > 0 && IsNumber(number) && (number[0] == '-');
    }
}
