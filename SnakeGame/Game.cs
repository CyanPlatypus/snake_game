using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    /*TODO:
     * -PlaceSnake
     * -Move (score) snake, delete food, bumping into obstacles and snake, finish
     * -put comments
     */
    

    class Game
    {
        static Random rnd = new Random();

        Coordinate2 foodCoorfinate;
        Snake snake;
        Board board;

        int score;

        public Game(int map = 1, int width = 50, int height = 50) 
        {
            board = new Board(map, width , height);
            snake = new Snake(new Coordinate2(width/3, height/3));
            score = 0;

            PlaceFood();
        }

        public int Score
        {
            get;
            set;
        }

        public Snake Snake
        {
            get { return snake; }
        }

        public Board Board
        {
            get { return board; }
        }

        //void PlaceWalls(int map, int width, int height) 
        //{
        //    SetAllCellsEmpty();
        //    switch (map) 
        //    {
        //        case 1: 
        //            {
        //                PlaceOneWall(0, width/2, height-1, false);
        //                break; 
        //            }
        //    }
        //}

        //void PlaceOneWall(int row, int col, int howMany, bool isHorisontal) 
        //{
        //    if (!isHorisontal)
        //    {
        //        Transpose(board);
        //    }
        //    for (int i = 0; i <= howMany; i++) 
        //    {
        //        board[col, i].Type = CellType.solid;
        //    }
        //    if (!isHorisontal)
        //    {
        //        Transpose(board);
        //    }
        //}

        void PlaceFood()
        {
            while (true)
            {
                int x = rnd.Next(board.BoardHeight);
                int y = rnd.Next(board.BoardWidht);

                if ((board.BoardArray[x,y].Type == CellType.empty) && (snake.FindCoordNumber(x,y) == -1))
                {
                    foodCoorfinate = new Coordinate2(x, y);
                    break;
                }
            }
        }

        public bool MoveSnake() 
        {
            bool fed = false;
            bool safe = true;
            snake.Move(board.BoardHeight, board.BoardWidht, foodCoorfinate, ref fed, ref safe);
            if (fed)
            {
                score++;
                PlaceFood();
            }
            if ((board.BoardArray[snake.HeadCoordinate.X, snake.HeadCoordinate.Y].Type == CellType.solid)
                || !safe)
                return false;
            else
                return true;
        }

        //void SetAllCellsEmpty()
        //{
        //    for (int i = 0; i < boardHeight; i++ )
        //    {
        //        for (int j = 0; j < boardWidth; j++)
        //        {
        //            board[i,j] = new Cell(CellType.empty);
        //        }
        //    }
        //}

        //static void Transpose<T>(T[,] arr) 
        //{
        //    for (int i = 0; i < arr.GetLength(0); i++) 
        //    {
        //        for (int j = i; j < arr.GetLength(1); j++)
        //        {
        //            T tmp = arr[i, j];
        //            arr[i, j] = arr[j, i];
        //            arr[j, i] = tmp;
        //        }
        //    }
        //}
    }
}
