using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    public enum Direction { up, right, down, left, no };
    class Snake
    {
        List<CoordinateXY> positions;
        Direction direction;

        public Snake(CoordinateXY head) 
        {
            positions = new List<CoordinateXY>();
            positions.Add(head);
            positions.Add(new CoordinateXY(head.X, head.Y-1));
            direction = Direction.no;
        }

        public CoordinateXY HeadCoordinate
        {
            get
            {
                return positions[0];
            }
            set
            {
                positions[0] = value;
            }
        }

        public CoordinateXY TailCoordinate
        {
            get
            {
                return positions.Last();
            }
            set
            {
                positions[positions.Count - 1] = value;
            }
        }

        public Direction Direction
        {
            set
            {
                if (((direction == Direction.up)&&(value != Direction.down ))
                    ||
                    ((direction == Direction.down)&&(value != Direction.up ))
                    ||
                    ((direction == Direction.right)&&(value != Direction.left ))
                    ||
                    ((direction == Direction.left) && (value != Direction.right))
                    || (direction == Direction.no))
                direction = value;
            }
        }

        public void Move(int height, int width, CoordinateXY foodCoordinate, ref bool fed, ref bool safe) 
        {
            switch (direction) 
            {
                case Direction.up: 
                    {
                        positions.Insert(0, new CoordinateXY((HeadCoordinate.X - 1 + height) % height, HeadCoordinate.Y));
                        break; 
                    }
                case Direction.down:
                    {
                        positions.Insert(0, new CoordinateXY((HeadCoordinate.X + 1 + height) % height, HeadCoordinate.Y));
                        break;
                    }
                case Direction.right:
                    {
                        positions.Insert(0, new CoordinateXY(HeadCoordinate.X, (HeadCoordinate.Y + 1 + width) % width));
                        break;
                    }
                case Direction.left:
                    {
                        positions.Insert(0, new CoordinateXY(HeadCoordinate.X, (HeadCoordinate.Y - 1 + width) % width));
                        break;
                    }
            }

            positions.RemoveAt(positions.Count - 1);

            if (HeadCoordinate == foodCoordinate)
            {
                positions.Insert(0, foodCoordinate);
                fed = true;
            }
            else 
                fed = false;
            if (HeadMetTail())
                safe = false;
            else
                safe = true;

        }

        bool HeadMetTail() 
        {
            for (int i = 3; i < positions.Count; i++) 
            {
                if (HeadCoordinate == positions[i])
                    return true;
            }
            return false;
        }

        public int FindCoordNumber(CoordinateXY position) 
        {
            return positions.IndexOf(position);
        }

        public int FindCoordNumber(int x, int y) 
        {
            //return positions.IndexOf(new CoordinateXY(x,y));
            return positions.FindIndex(r => ((r.X == x) && (r.Y == y)));
        }
    }
}
