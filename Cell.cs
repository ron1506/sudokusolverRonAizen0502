using System;
using System.Collections.Generic;

namespace sudokusolverRonAizen
{
    /// <summary>
    /// represents each cell in the board.
    /// </summary>
    public class Cell
    {
        public int value { get; set; } // the value of the cell
        public int row { get; set; } // the row of the cell in the board.
        public int col { get; set; } // thr column of the cell in the board.
        public List<int> posibilities { get; set; } // a list with all the cell's possibilties.

        /// <summary>
        /// constructor, initializing the cell's attributes.
        /// </summary>
        /// <param name="row">the row of the cell in the board.</param>
        /// <param name="col">the col of the cell in the board.</param>
        /// <param name="value">the value of the cell</param>
        public Cell(int row, int col, int value)
        {
            this.value = value;
            this.posibilities = new List<int>();
            this.row = row;
            this.col = col;
        }
        /// <summary>
        /// copy constructor, initializing the cell's attributes, with tha other cell attributes.
        /// </summary>
        /// <param name="cell"></param>
        public Cell(Cell cell)
        {
            this.row = cell.row;
            this.col = cell.col;
            this.posibilities = new List<int>();
            this.value = cell.value;
        }
    }
}
