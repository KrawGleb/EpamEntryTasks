using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTask
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(string point)
        {
            if (point.Length != 2 || !char.IsLetter(point[0]) || !char.IsDigit(point[^1]))
            {
                throw new ArgumentException(nameof(point));
            }

            X = (int)point[0] - 65;
            if (X < 0 || X > 7)
            {
                throw new ArgumentException(nameof(point));
            }

            Y = int.Parse(point[^1].ToString()) - 1;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }

        public static bool operator ==(Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator !=(Point p1, Point p2) => p1.X != p2.X || p1.Y != p2.Y;
    }
}
