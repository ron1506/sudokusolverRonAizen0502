using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sudokusolverRonAizen
{
    /// <summary>
    /// console outputer.
    /// </summary>
    public class ConsoleReader : IReader
    {
        /// <summary>
        /// reading the values of board from the console and returning them in a string format.
        /// </summary>
        /// <returns>the value of the board in a string format.</returns>
        public string Read()
        {
            Console.Write("Enter the numbers of the sudoku: ");
            byte[] inputBuffer = new byte[1024];
            Stream inputStream = Console.OpenStandardInput(inputBuffer.Length);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));
            return Console.ReadLine(); 
        }
    }
}
