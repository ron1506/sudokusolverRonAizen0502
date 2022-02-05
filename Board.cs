using System;
using System.Collections.Generic;
using System.Text;

namespace sudokusolverRonAizen
{
    /// <summary>
    ///  represents the sudoku board.
    /// </summary>
    public class Board
    {
        private static List<int> sizes = new List<int> { 1, 4, 9, 16 }; // a list with the possibile sizes.
        private string content; // the values of the board in a string.
        public Cell[,] sudoku_board; // the matrix that contains all the cells in the board.
        public Cell this [int i, int j] // enables reaching the board.sudoku_board[i, j] with the statment board[i, j].
        {
            get { return sudoku_board[i, j]; }
            set { this.sudoku_board[i, j] = value;  }
        }
        public int size { get; set; } // the size of the board

        /// <summary>
        /// constructor, initializing the board's attributes. 
        /// </summary>
        /// <param name="content">the values of the sudoku board i string format.</param>
        public Board(string content)
        {
            this.content = content;
            this.size = (int)Math.Sqrt(content.Length); // the size of the board equals to the square root of the content's length.
            sudoku_board = new Cell[size, size];
        }

        /// <summary>
        /// Copy constructor, creates new instance with the board's same identical attribures.
        /// </summary>
        /// <param name="board">the board to copy.</param>
        public Board(Board board)
        {
            this.size = board.size;
            this.sudoku_board = new Cell[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    sudoku_board[row, col] = new Cell(board[row, col]);
                }
            }
            this.content = board.content;
        }

        /// <summary>
        /// initializing the board cells. 
        /// </summary>
        public void Init_board()
        {
            int str_index = 0;
            for (int i = 0; i < size; i++) // initializing all the matrix's cells.
            {
                for (int j = 0; j < size; j++)
                {
                    this.sudoku_board[i, j] = new Cell(i, j, content[str_index++] - '0'); // creating a new cell with tha value fron the content string.
                }
            }
        }
        /// <summary>
        /// recieves the cell with the minimum amount of possibilies, an da possibility, and changes the value of this apecific cell in the board.
        /// </summary>
        /// <param name="minCell">the cell with the minimum amount of possibilies in the board.</param>
        /// <param name="possibility">its new value.</param>
        public void changeCellInBoard(Cell minCell, int possibility)
        {
            sudoku_board[minCell.row, minCell.col] = new Cell(minCell.row, minCell.col, possibility);
        }

        /// <summary>
        /// the function prints the board.
        /// </summary>
        public void printBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((j + 1) % ((int)Math.Sqrt(size)) == 0 && (j + 1) != size)
                    {
                        if (sudoku_board[i, j].value >= 10)//2 digits
                            Console.Write(" " + sudoku_board[i, j].value + "|");
                        else
                            Console.Write(" " + sudoku_board[i, j].value + " |");
                    }
                    else if (j + 1 == size)
                    {
                        if (sudoku_board[i, j].value >= 10)//2 digits
                            Console.Write(" " + sudoku_board[i, j].value);
                        else
                            Console.Write(" " + sudoku_board[i, j].value);
                    }
                    else
                    {
                        if (sudoku_board[i, j].value >= 10)//2 digits
                            Console.Write(" " + sudoku_board[i, j].value + ",");
                        else
                            Console.Write(" " + sudoku_board[i, j].value + ", ");
                    }
                }
                if ((i + 1) % ((int)Math.Sqrt(size)) == 0 && (i + 1) != size)
                {
                    print_between_cubes(((int)Math.Sqrt(size)));
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// helping method for printBoard method.
        /// </summary>
        /// <param name="size"></param>
        private void print_between_cubes(int size)
        {
            Console.WriteLine();
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size * 4 - 1; i++)
                {
                    Console.Write('-');
                }
                if (j < size - 1)
                    Console.Write('+');
            }
        }

        public override string ToString()
        {
            string boardstr = "";
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    boardstr += (char)('0' + sudoku_board[row, col].value);
                }
            }
            return boardstr;
        }
    }
}
