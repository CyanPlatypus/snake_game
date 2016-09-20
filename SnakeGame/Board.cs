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

        public Board(int map , int width , int height ) 
        {
            boardHeight = height;
            boardWidth = width;
            board = new Cell[boardHeight, boardWidth];

            PlaceWalls(map, width, height);
        }

        public int BoardWidth { get { return boardWidth; } }

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
            if (isHorisontal)
            {
                for (int i = 0; i < howMany; i++)
                {
                    board[row, i + col].Type = CellType.solid;
                }
            }
            else
            {
                for (int i = 0; i < howMany; i++)
                {
                    board[i + row, col].Type = CellType.solid;
                }
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
    }
}
