using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    /*TODO:
     * -PlaceWalls
     * -PlaceFood
     * -Move (score) snake, delete food, bumping into obstacles and snake, finish
     * -put comments
     * -property for board
     */
    

    class Board
    {
        static Random rnd = new Random();

        int boardWidth;
        int boardHeight;
        Cell[,] board;
        Coordinate2 foodCoorfinate;
        Snake snake;

        int score;

        public Board(int map = 1, int width = 50, int height = 30) 
        {
            boardHeight = height;
            boardWidth = width;
            board = new Cell[boardHeight, boardWidth];
            snake = new Snake(new Coordinate2(width/2, height/2));
            score = 0;

            PlaceWalls(map, width, height);
            PlaceFood();
        }

        public int Score
        {
            get;
            set;
        }

        void PlaceWalls(int map, int width, int height) 
        {
            SetAllCellsEmpty();
            switch (map) 
            {
                case 1: 
                    {
                        PlaceOneWall(0, width/2, height-1, false);
                        break; 
                    }
            }
        }

        void PlaceOneWall(int row, int col, int howMany, bool isHorisontal) 
        {
            if (isHorisontal)
            {
                Transpose(board);
            }
            for (int i = col; i <= howMany; i++) 
            {
                board[row, i].Type = CellType.solid;
            }
            if (isHorisontal)
            {
                Transpose(board);
            }
        }

        void PlaceFood()
        {
            while (true)
            {
                int x = rnd.Next(boardHeight);
                int y = rnd.Next(boardWidth);

                if ((board[x,y].Type == CellType.empty) && (snake.FindCoordNumber(x,y) == -1))
                {
                    foodCoorfinate = new Coordinate2(x, y);
                }
            }
        }

        public bool MoveSnake() 
        {
            bool fed = false;
            bool safe = true;
            snake.Move(boardHeight, boardWidth, foodCoorfinate, ref fed, ref safe);
            if (fed)
            {
                score++;
                PlaceFood();
            }
            if ((board[snake.HeadCoordinate.X, snake.HeadCoordinate.Y].Type == CellType.solid)
                || !safe)
                return false;
            else
                return true;
        }

        void SetAllCellsEmpty()
        {
            for (int i = 0; i < boardHeight; i++ )
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    board[i,j] = new Cell(CellType.empty);
                }
            }
        }

        static void Transpose<T>(T[,] arr) 
        {
            for (int i = 0; i < arr.GetLength(0); i++) 
            {
                for (int j = i; j < arr.GetLength(1); j++)
                {
                    T tmp = arr[i, j];
                    arr[i, j] = arr[j, i];
                    arr[j, i] = tmp;
                }
            }
        }
    }
}
