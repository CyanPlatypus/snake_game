using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    class CoordinateXY
    {
        int x;
        int y;

        public CoordinateXY(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
        public int X { get { return x; } set { x = value;} }
        public int Y { get { return y; } set { y = value; } }

        public static CoordinateXY operator +(CoordinateXY fst, CoordinateXY scnd) 
        {
            return new CoordinateXY(fst.X + scnd.X, fst.Y + scnd.Y);
        }

        public static bool operator ==(CoordinateXY fst, CoordinateXY scnd)
        {
            return ((fst.X == scnd.X) && (fst.Y == scnd.Y));
        }
        public static bool operator !=(CoordinateXY fst, CoordinateXY scnd)
        {
            return !((fst.X == scnd.X) && (fst.Y == scnd.Y));
        }
    }
}
