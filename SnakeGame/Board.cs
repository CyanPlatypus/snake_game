using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    class Board
    {
        int boardWidth;
        int boardHeight;
        Cell[,] board;

        public Board(int map = 1, int width = 50, int height = 50) 
        {
            boardHeight = height;
            boardWidth = width;
            board = new Cell[boardHeight, boardWidth];

            PlaceWalls(map, width, height);
            //PlaceFood();
        }

        public int BoardWidht { get { return boardWidth; } }

        public int BoardHeight { get { return boardHeight; } }

        public Cell[,] BoardArray
        {
            get { return board; }
        }

        void PlaceWalls(int map, int width, int height)
        {
            SetAllCellsEmpty();
            switch (map)
            {
                case 1:
                    {
                        PlaceOneWall(0, width / 2 , height , false);
                        break;
                    }
            }
        }

        void PlaceOneWall(int row, int col, int howMany, bool isHorisontal)
        {
            if (!isHorisontal)
            {
                int tmp = col;
                col = row;
                row = tmp;
                Transpose(board);
            }
            for (int i = 0; i < howMany; i++)
            {
                board[row, i + col].Type = CellType.solid;
            }
            if (!isHorisontal)
            {
                Transpose(board);
            }
        }

        void SetAllCellsEmpty()
        {
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    board[i, j] = new Cell(CellType.empty);
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
