using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sudokusolverRonAizen
{
    public class Program
    {
        /// <summary>
        /// the function recieves a mode(file or console) and 
        /// </summary>
        /// <param name="mode", type=char>
        /// 'f'- for reading from file.
        /// 'c'- for reading from the console.
        /// </param>
        /// <param name="path"></param>
        /// <returns>the recieved string tht contains the board parameters.</returns>
        public static string InputOfString(char mode, ref string path)
        {
            string content = null;
            IReader reader;
            switch (mode)
            {
                case 'f':
                case 'F':
                    reader = new FileReader();
                    break;
                case 'c':
                case 'C':
                default:
                    reader = new ConsoleReader();
                    break;
            }
            try
            {
                content = reader.Read();
                if (mode == 'f')
                {
                    path = ((FileReader)reader).FilePath; // initializing the path of the file.
                }
                return content;
            }
            catch (FileNotFoundException e) // in case the file was not found, just to safe even though it doesn't go there with the filedialog.
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (FileChoosingException e) // in case there was a problem with opening the file.
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        /// <summary>
        /// recieveing the content of the board as a string and checks if:
        /// 1) if the size is valid, orherwise catching InvalidSizeException raised from the validaitor's check size function.
        /// 2) if the values in the rows are valid, orherwise catching InvalidValuesException raised from the validaitor's check rows function.
        /// 3) if the values in the cols are valid, orherwise catching InvalidValuesException raised from the validaitor's check cols function.
        /// 4) if the values in the rows are valid, orherwise catching InvalidValuesException raised from the validaitor's check rows function.
        /// 5) if the values in the cubes are valid, orherwise catching InvalidValuesException raised from the validaitor's check cubes function.
        /// </summary>
        /// <param name="content"> type=string. the values of the sudoku board in string format.</param>
        /// <returns> true if the values are valid and the size is valid as well. </returns>
        public static bool ValidatingInput(string content)
        {
            if (content == null)
                return false;
            Validator validator = new Validator(content);
            try
            {
                validator.check_size();
                validator.valid_cols();
                validator.valid_rows();
                validator.valid_values();
                validator.valid_cubes();
                return true;
            }
            catch (Exception e) // otherwise InvalidValuesException or InvalidSizeException.
            {
                Console.WriteLine(e.Message);  // prints the exception description for the user.
                return false;
            }
        }
        /// <summary>
        /// the function gets the string content of the sudoku board and initializing the board
        /// </summary>
        /// <param name="content">type=string. the values of the sudoku board in string format.</param>
        /// <returns>the board object initialized.</returns>
        public static Board CreateBoard(string content)
        {
            Board board = new Board(content); // constructor.
            board.Init_board(); // the board initializor function.
            return board;
        }
        /// <summary>
        /// the function solves the recieved board.
        /// </summary>
        /// <param name="board">the board after initializing.</param>
        /// <returns>the solved board if solvable, otherwise null.</returns>
        public static Board SolveBoard(Board board)
        {
            Solver solver = new Solver(board);
            if (solver.solveBackTrack()) // the main solving function, returns true if board has been solved, false othrwise
                // the function also changes the values of the board in ths solveBackTrack method.
            {
                board = solver.board; // updating the board.
                return board;
            }
            return null; // in case the board is not solvable.
        }
        /// <summary>
        /// the funtion gets the solvedor initial board and returns it to the user in the same way he entered the values.
        /// </summary>
        /// <param name="board">either the solved board, or null in case the board is not solvable.</param>
        /// <param name="mode">the mode of returning the board.</param>
        /// <param name="message">an indicative meassage about the board's status.</param>
        /// <param name="path">if mode is 'f' the path of the origin file othewise null</param>
        public static void OutputOfSolution(Board board, char mode, string message, string path = null)
        {
            IOutputer outputer;
            switch (mode)
            {
                case 'f':
                case 'F':
                    outputer = new FileOutputer(path);
                    break;
                case 'c':
                case 'C':
                default:
                    outputer = new ConsoleOutputer();
                    break;
            }
            if (board == null) // in case the board is not solveable.
            {
                outputer.Output(null);
            }
            else
            {
                try
                {
                    Console.WriteLine("=====================================");
                    Console.WriteLine(message);
                    Console.WriteLine("=====================================");
                    outputer.Output(board);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\nfile was not found.");
                }
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Board board;
            bool finish = false; // whether to stop flag.
            string content = null, path = null; // the initialized content of and path.
            Console.WriteLine("Welcome to the Sudoku Solver!");
            while (!finish)
            {
                Console.WriteLine("please enter 'f' if you want the values to be scanned from a file and c to be read from the console.");
                try
                {
                    char mode = char.Parse(Console.ReadLine());
                    content = InputOfString(mode, ref path); //calling the function that recieves the content
                    if (ValidatingInput(content)) // if the input is valid.
                    {
                        board = CreateBoard(content); // creating and initilizing the board.
                        //**********************************************
                        OutputOfSolution(board, 'c', "THE GIVEN BOARD: "); // printing the given board.
                        //**********************************************
                        stopwatch.Start();// starting the clock.
                        board = SolveBoard(board); // sending the board to the sudoku solver
                        Console.WriteLine(board.ToString());
                        OutputOfSolution(board, mode, "THE SOLVED BOARD: ", path); //path=null, in case the mode the mode is not 'f'.
                        stopwatch.Stop();
                        Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();
                    }
                    Console.WriteLine("\nwould you like to enter another soduko?");
                    if (Console.ReadLine().ToUpper().Contains("NO")) // if no was entered in any form some how.
                        finish = true;
                    content = null;
                }
                catch (Exception e) // if there was any problem in the program.
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
    }
}
