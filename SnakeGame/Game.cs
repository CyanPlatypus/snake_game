using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    /*TODO:
     * -put comments
     */
    

    class Game
    {
        static Random rnd = new Random();

        CoordinateXY foodCoorfinate;
        Snake snake;
        Board board;

        int score;

        public Game(int map = 1, int width = 40, int height = 40) 
        {
            board = new Board(map, width , height);
            snake = new Snake(new CoordinateXY(width/3, height/3));
            score = 0;

            PlaceFood();
        }

        public int Score
        {
            get { return score;}
        }

        public Snake Snake
        {
            get { return snake; }
        }

        public Board Board
        {
            get { return board; }
        }

        public CoordinateXY FoodCoordinate 
        {
            get {return foodCoorfinate;}
        }

        void PlaceFood()
        {
            while (true)
            {
                int x = rnd.Next(board.BoardHeight);
                int y = rnd.Next(board.BoardWidth);

                if ((board.BoardArray[x,y].Type == CellType.empty) && (snake.FindCoordNumber(x,y) == -1))
                {
                    foodCoorfinate = new CoordinateXY(x, y);
                    break;
                }
            }
        }

        public void MoveSnake(ref bool fed, ref bool safe) 
        {
            snake.Move(board.BoardHeight, board.BoardWidth, foodCoorfinate, ref fed, ref safe);
            if (fed)
            {
                score++;
                PlaceFood();
            }
            if (board.BoardArray[snake.HeadCoordinate.X, snake.HeadCoordinate.Y].Type == CellType.solid)
                 safe = false;
        }


    }
}
