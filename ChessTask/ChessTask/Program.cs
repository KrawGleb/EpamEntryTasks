using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start point: ");
            string startPoint = Console.ReadLine().Trim().ToUpper();

            Console.WriteLine("End point: ");
            string endPoint = Console.ReadLine().Trim().ToUpper();

            Point start = new Point(startPoint);
            Point end = new Point(endPoint);

            Console.WriteLine(GetMovesCount(start, end));
            Console.ReadKey();
        }

        private static int GetMovesCount(Point startPoint, Point endPoint)
        {
            if (startPoint == endPoint)
                return 0;

            if (CheckUnitArea(startPoint, endPoint) != 0)
                return CheckUnitArea(startPoint, endPoint);
            
            string direction = "";
            direction += startPoint.Y <= endPoint.Y ? "N" : "S";
            direction += startPoint.X <= endPoint.X ? "E" : "W";

            int result = 10000;

            foreach (var nextPoint in GetPossibleMoves(startPoint, direction))
                result = Math.Min(result, 1 + GetMovesCount(nextPoint, endPoint));

            return result;
        }

        private static int CheckUnitArea(Point startPoint, Point endPoint)
        {
            if (Math.Abs(startPoint.X - endPoint.X) <= 1 && Math.Abs(startPoint.Y - endPoint.Y) <= 1)
                return (startPoint.X != endPoint.X && startPoint.Y != endPoint.Y) ? 2 : 3;

            return 0;
        }

        private static IEnumerable<Point> GetPossibleMoves(Point startPoint, string direction)
        {
            Point horizontalMove;
            Point verticalMove;

            switch (direction.ToUpper())
            {
                case "NE":
                case "EN":
                    horizontalMove = new Point(startPoint.X + 2, startPoint.Y + 1);
                    verticalMove = new Point(startPoint.X + 1, startPoint.Y + 2);
                    break;

                case "NW":
                case "WN":
                    horizontalMove = new Point(startPoint.X - 2, startPoint.Y + 1);
                    verticalMove = new Point(startPoint.X - 1, startPoint.Y + 2);
                    break;

                case "SW":
                case "WS":
                    horizontalMove = new Point(startPoint.X - 2, startPoint.Y - 1);
                    verticalMove = new Point(startPoint.X - 1, startPoint.Y - 2);
                    break;

                case "SE":
                case "ES":
                    horizontalMove = new Point(startPoint.X + 2, startPoint.Y - 1);
                    verticalMove = new Point(startPoint.X + 1, startPoint.Y - 2);
                    break;

                default:
                    throw new ArgumentException("Invalid direction", nameof(direction));
            }

            if (0 <= horizontalMove.X && horizontalMove.X < 8)
                yield return horizontalMove;

            if (0 <= verticalMove.X && verticalMove.X < 8)
                yield return verticalMove;
        }
    }
}
