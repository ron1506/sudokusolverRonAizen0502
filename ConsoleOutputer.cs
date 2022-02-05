using System;
using System.Collections.Generic;
using System.Text;

namespace sudokusolverRonAizen
{
    public class ConsoleOutputer : IOutputer
    {
        /// <summary>
        /// Printing the board to the console.
        /// </summary>
        public void Output(Board board)
        {
            if (board == null)
                Console.WriteLine("the board is not solvable");
            else
            {
                board.printBoard();
            }

        }
    }
}
