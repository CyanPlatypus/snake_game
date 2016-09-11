using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    public enum Direction { up, right, down, left };
    class Snake
    {
        List<Coordinate2> positions;
        Direction direction;

        public Snake(Coordinate2 head) 
        {
            positions.Add(head);
            positions.Add(new Coordinate2(head.X-1, head.Y));
        }

        public Coordinate2 HeadCoordinate
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

        public Coordinate2 TailCoordinate
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
                    ((direction == Direction.left) && (value != Direction.right)))
                direction = value;
            }
        }

        public void Move(int height, int width, Coordinate2 foodCoordinate, ref bool fed, ref bool safe) 
        {
            switch (direction) 
            {
                case Direction.up: 
                    {
                        positions.Insert(0, new Coordinate2((HeadCoordinate.X - 1)% height, HeadCoordinate.Y));
                        break; 
                    }
                case Direction.down:
                    {
                        positions.Insert(0, new Coordinate2((HeadCoordinate.X + 1) % height, HeadCoordinate.Y));
                        break;
                    }
                case Direction.right:
                    {
                        positions.Insert(0, new Coordinate2(HeadCoordinate.X, (HeadCoordinate.X + 1) % width));
                        break;
                    }
                case Direction.left:
                    {
                        positions.Insert(0, new Coordinate2(HeadCoordinate.X, (HeadCoordinate.X - 1) % width));
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

        public int FindCoordNumber(Coordinate2 position) 
        {
            return positions.IndexOf(position);
        }

        public int FindCoordNumber(int x, int y) 
        {
            return positions.IndexOf(new Coordinate2(x,y));
        }
    }
}
