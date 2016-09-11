using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    public enum CellType { empty, solid, food };

    class Cell
    {
        CellType type;

        public Cell(CellType type) 
        {
            this.type = type;
        }

        public CellType Type
        {
            get 
            {
                return type;
            }
            set 
            {
                type = value;
            } 
        }
    }
}
