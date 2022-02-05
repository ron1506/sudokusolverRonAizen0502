using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace sudokusolverRonAizen
{
    public class FileOutputer : IOutputer
    {
        private string path;

        /// <summary>
        /// constructor, initializing the FileOutputer's attributes.
        /// </summary>
        /// <param name="path">the path of the file we want to return the solved board to.</param>
        public FileOutputer(string path)
        {
            this.path = path;
        }
        /// <summary>
        /// Printing the board to the console.
        /// and writing the solved board in the file.
        /// </summary>
        /// <param name="size">the size of the board.</param>
        /// <param name="board">the board itself</param>
        public void WriteToFile(int size, Cell[,] board)
        {
            string content = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    content += (char)(board[i, j].value + '0');
                }
            }
            File.WriteAllText(path, content);
        }
        /// <summary>
        /// Printing the board to the console. and writing the solution to the file.
        /// </summary>
        /// <param name="board">the solved board.</param>
        public void Output(Board board)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            else if (board == null)
            {
                Console.WriteLine("the board is not solvable");
            }
            else
            {
                board.printBoard();
                //--------------------------------------------
                Console.WriteLine("would you like to save the solution to the origin file? ");
                string toSave;
                toSave = Console.ReadLine();
                switch (toSave.ToUpper())
                {
                    case "NO":
                        break;
                    case "YES":
                    default:
                        WriteToFile(board.size, board.sudoku_board);
                        break;

                }
                //-------------------------------------------

            }
        }
    }
}
