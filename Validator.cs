using System;
using System.Collections.Generic;
using System.Text;

namespace sudokusolverRonAizen
{
    /// <summary>
    /// the class that validates the board's values.
    /// </summary>
    public class Validator
    {
        protected static List<int> sizes = new List<int> { 1, 4, 9, 16, 25}; // the possible sizes.
        private string content; // the values of the board in a string.
        public int size { get; set; } // the size of the board.
        /// <summary>
        /// constructor, initializing the validator attributes.
        /// </summary>
        /// <param name="content">the board's vlaues in string format.</param>
        public Validator(string content)
        {
            this.content = content;
            this.size = (int)Math.Sqrt(content.Length);
        }

        /// <summary>
        /// the function checks if the size is valid if not throws InvalidSizeException.
        /// </summary>
        public void check_size()
        {
            bool size_valid = false;
            foreach (int i in sizes)
            {
                if (content.Length == (i * i))
                    size_valid = true;
            }
            if(!size_valid)
                throw new InvalidSizeException("unappropriate size.");
        }

        /// <summary>
        /// the function checks if all the vlues in the board are valid and if not throws InvalidValuesException.
        /// </summary>
        public void valid_values()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (content[(i * size + j)] - '0' > size || content[(i * size + j)] - '0' < 0) // if the value is greater than the board's size and less than 0.
                        throw new InvalidValuesExceptions("values are not in range.");
                }
            }
        }

        /// <summary>
        /// the function checks if all the vlues in the rows are legal and if not throws InvalidValuesException.
        /// </summary>
        public void valid_rows()
        {
            int[] monim = new int[size]; //an array that will count the amount of numbers board.
            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++) //putting 0 in the array
                {
                    monim[k] = 0;
                }
                for (int j = 0; j < size; j++)
                {
                    if (content[(i * size + j)] - '0' > 0 && content[(i * size + j)] - '0' <= size) // taken place
                        monim[content[(i * size + j)] - '0' - 1]++; // add 1 in the array monim where the value in the string minus one is the position in the array.
                }
                for (int k = 0; k < size; k++)
                {
                    if (monim[k] > 1) // if there are 2 identical numbers in the same row.
                        throw new InvalidValuesExceptions("2 identical numbers in one row.");
                }
            }
        }

        /// <summary>
        /// the function checks if all the vlues in the cols are valid and if not throws InvalidValuesException.
        /// </summary>
        public void valid_cols()
        {
            int[] monim = new int[size]; //an array that will count the amount of numbers board.
            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++) //putting 0 in the array
                {
                    monim[k] = 0;
                }
                for (int j = 0; j < size; j++)
                {
                    if (content[(j * size + i)] - '0' > 0 && content[(j * size + i)] - '0' <= size) // taken place
                        monim[content[(j * size + i)] - '0' - 1]++; // add 1 in the array monim where the value in the string minus one is the position in the array.
                }
                for (int k = 0; k < size; k++)
                {
                    if (monim[k] > 1) // if there are 2 identical numbers in the same col.
                        throw new InvalidValuesExceptions("2 identical numbers in one column.");
                }
            }
        }
        /// <summary>
        /// the function checks if all the vlues in the cubes are valid and if not throws InvalidValuesException.
        /// </summary>
        public void valid_cubes()
        {
            int squareroot = (int)Math.Sqrt(size);
            for (int rowStart = 0; rowStart < size; rowStart += squareroot) // the amount of cubes in one board, in a size of (size X size) is size.
            {
                for (int colStart = 0; colStart < size; colStart += squareroot)
                {
                    int[] monim = new int[size]; //an array that will count the amount of numbers board.
                    for (int i = rowStart; i < rowStart + squareroot; i++)
                        for (int j = colStart; j < colStart + squareroot; j++)
                            if (content[(i * size + j)] - '0' > 0 && content[(i * size + j)] - '0' <= size) // taken place
                                monim[content[(i * size + j)] - '0' - 1]++; // add 1 in the array monim where the value in the string minus one is the position in the array.
                    for (int i = 0; i < size; i++)
                    {
                        if (monim[i] > 1) // if there are 2 identical numbers in the same cube.
                            throw new InvalidValuesExceptions("2 identical numbers in one cube.");
                    }
                }
            }
        }
    }
}
