using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    class Coordinate2
    {
        int x;
        int y;

        public Coordinate2(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public static Coordinate2 operator +(Coordinate2 fst, Coordinate2 scnd) 
        {
            return new Coordinate2(fst.X + scnd.X, fst.Y + scnd.Y);
        }
    }
}
