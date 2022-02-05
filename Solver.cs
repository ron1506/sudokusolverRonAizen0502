using System;
using System.Collections.Generic;
using System.Linq;

namespace sudokusolverRonAizen
{
    /// <summary>
    /// the Sudoku Solver.
    /// </summary>
    public class Solver
    {
        public Board board; // the Object board.
        private int howManyEmptyCells;

        /// <summary>
        /// constructor initializing the solver's attributes.
        /// </summary>
        /// <param name="board"></param>
        public Solver(Board board)
        {
            this.board = board;
            this.howManyEmptyCells = amountOfEmptyCells();
        }

        /// <summary>
        /// recieve's a cell and returns true if its value is in the row and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false</returns>
        public bool check_in_row(Cell cell)
        {
            for (int i = 0; i < board.size; i++)
                if (board[cell.row, i].value == cell.value)
                    return true;
            return false;
        }

        /// <summary>
        /// recieve's a cell and returns true if its value is in the col and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false</returns>
        public bool check_in_col(Cell cell)
        {
            for (int i = 0; i < board.size; i++)
                if (board[i, cell.col].value == cell.value)
                    return true;
            return false;
        }

        /// <summary>
        /// recieve's a cell and returns true if its value is in the cube and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false. </returns>
        public bool check_in_cube(Cell cell)
        {
            int squareroot = (int)Math.Sqrt(board.size);
            int rowStart = cell.row - cell.row % squareroot;
            int colStart = cell.col - cell.col % squareroot;

            for (int i = rowStart; i < rowStart + squareroot; i++)
                for (int j = colStart; j < colStart + squareroot; j++)
                    if (board[i, j].value == cell.value)
                        return true;

            return false;
        }

        /// <summary>
        /// recieve's a cell and returns true if the cell can be a legal cell in the board and false otherwise. 
        /// legal cell: which means that its value doesn' appears in its row, col and cube.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false.</returns>
        private bool canBePlaced(Cell cell)
        {
            return !(check_in_row(cell) || check_in_col(cell) || check_in_cube(cell));
        }

        /// <summary>
        /// initializing the cell's possibilities list.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        public void initializing_posibilities(Cell cell)
        {
            cell.posibilities.Clear();
            for (int number = 1; number <= board.size; number++) // goes on all the possibilities from 1 to size.
                if (canBePlaced(new Cell(cell.row, cell.col, number))) // if can be placed than added.
                    cell.posibilities.Add(number);
        }

        /// <summary>
        /// return's the cell's possibilitties list.
        /// </summary>
        /// <param name="cell">the checked cell.</param>
        /// <returns>the cell's possibility list.</returns>
        public List<int> findPossibilities(Cell cell)
        {
            initializing_posibilities(cell);
            return cell.posibilities;
        }

        /// <summary>
        /// the fuction finds the cell with the list possibilities and returns it.
        /// </summary>
        /// <returns>the cell with the least possibilities.</returns>
        public Cell FindMinCell()
        {
            int minPossibilities = board.size + 1; // the  max number of possibilities + 1.
            Cell minCell = null;
            Cell currCell;
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    if (board[row, col].value == 0) // if the cell is empty, its value is not taken yet.
                    {
                        currCell = new Cell(row, col, 0); // copy of the cell
                        List<int> possibilities = findPossibilities(currCell); // finding the cell's possibilities.
                        if (possibilities.Count < minPossibilities) // if the current cells possibilities is little than the minCell possibilities.
                        {
                            minCell = currCell; // updating the minCell
                            minPossibilities = possibilities.Count; //updating the size of the possibilities.
                            minCell.posibilities = possibilities; // updating the cell's possibilities.
                        }
                    }
                }
            }
            return minCell;
        }

        /// <summary>
        /// if the cell contains one possibility than put that possibilty in its value and return true, otherwise return false.
        /// </summary>
        /// <param name="cell">the checked cell.</param>
        /// <returns>true or false.</returns>
        public bool naked_single(Cell cell)
        {
            initializing_posibilities(cell); // updating the cell's possibilities.
            if (cell.posibilities.Count == 1)
            {
                cell.value = cell.posibilities[0]; // updating the cell's value to the possibility.
                return true;
            }
            if (cell.posibilities.Count == 0)
                throw new UnsolveableBoardException("the board is not solveable");
            return false;
        }

        /// ***************************HIDDEN SINGLE-MEANING***************************
        /// Hidden Single means that for a given digit and house only one cell is left to place that digit. 
        /// The cell itself has more than one candidate left, the correct digit is thus hidden amongst the rest.
        /// ******************************************************

        /// <summary>
        /// returns true if there is a hidden single in the row and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false</returns>
        public bool hidden_single_in_row(Cell cell)
        {
            List<int> possibilities = new List<int>();
            for (int i = 0; i < board.size; i++) // iterator of the cols in spesific row.
            {
                if (i != cell.col && board[cell.row, i].value == 0) //if not the curr cell and an empty cell.
                {
                    initializing_posibilities(board[cell.row, i]); // updating the cell's possibilities.
                    possibilities.AddRange(board[cell.row, i].posibilities); // adding its possibilities to the entire cube's possibilities.
                }
            }
            possibilities = possibilities.Distinct().ToList();
            List<int> hiddens = new List<int>();
            foreach (int possibility in cell.posibilities) // going on the cell's possibilities.
            {
                if (!possibilities.Contains(possibility)) // if one of the cell's possibilities is not in the entire row possibilities than put that possibility in the cell's value.
                {
                    hiddens.Add(possibility);
                }
            }
            if (hiddens.Count == 0) // no hiddens than return false.
                return false;
            if (hiddens.Count == 1) // only one hidden than return true and change the cell's value.
            {
                board.changeCellInBoard(cell, hiddens[0]);
                return true;
            }
            throw new UnsolveableBoardException("the board is not solveable"); // more than 1 hidden than throw UnsolveableBoardException.
        }

        /// <summary>
        /// returns true if there is a hidden single in the col and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false</returns>
        public bool hidden_single_in_col(Cell cell)
        {
            List<int> possibilities = new List<int>();
            for (int i = 0; i < board.size; i++) // iterator of the rows in spesific col.
            {
                if (i != cell.row && board[i, cell.col].value == 0) //if not the curr cell and an empty cell.
                {
                    initializing_posibilities(board[i, cell.col]); // updating the cell's possibilities.
                    possibilities.AddRange(board[i, cell.col].posibilities); // adding its possibilities to the entire cube's possibilities.
                }
            }
            possibilities = possibilities.Distinct().ToList();
            List<int> hiddens = new List<int>();
            foreach (int possibility in cell.posibilities) // going on the cell's possibilities.
            {
                if (!possibilities.Contains(possibility)) // if one of the cell's possibilities is not in the entire row possibilities than put that possibility in the cell's value.
                {
                    hiddens.Add(possibility);
                }
            }
            if (hiddens.Count == 0) // no hiddens than return false.
                return false;
            if (hiddens.Count == 1) // only one hidden than return true and change the cell's value.
            {
                board.changeCellInBoard(cell, hiddens[0]);
                return true;
            }
            throw new UnsolveableBoardException("the board is not solveable"); // more than 1 hidden than throw UnsolveableBoardException.
        }

        /// <summary>
        /// returns true if there is a hidden single in the box and false otherwise.
        /// </summary>
        /// <param name="cell">the checked cell</param>
        /// <returns>true or false</returns>
        public bool hidden_single_in_box(Cell cell)
        {
            int squareroot = (int)Math.Sqrt(board.size);
            int rowStart = cell.row - cell.row % squareroot;
            int colStart = cell.col - cell.col % squareroot;
            List<int> possibilities = new List<int>();
            for (int i = rowStart; i < rowStart + squareroot; i++) //going on the entire cube.
            {
                for (int j = colStart; j < colStart + squareroot; j++)
                {
                    if ((!(i == cell.row && j == cell.col)) && board[i, j].value == 0) //if not the curr cell and an empty cell.
                    {
                        initializing_posibilities(board[i, j]); // updating the cell's possibilities.
                        possibilities.AddRange(board[i, j].posibilities); // adding its possibilities to the entire cube's possibilities.
                    }
                }
            }
            possibilities = possibilities.Distinct().ToList();
            List<int> hiddens = new List<int>();
            foreach (int possibility in cell.posibilities) // going on the cell's possibilities.
            {
                if (!possibilities.Contains(possibility)) // if one of the cell's possibilities is not in the entire row possibilities than put that possibility in the cell's value.
                {
                    hiddens.Add(possibility);
                }
            }
            if (hiddens.Count == 0) // no hiddens than return false.
                return false;
            if (hiddens.Count == 1) // only one hidden than return true and change the cell's value.
            {
                board.changeCellInBoard(cell, hiddens[0]);
                return true;
            }
            throw new UnsolveableBoardException("the board is not solveable"); // more than 1 hidden than throw UnsolveableBoardException.
        }

        /// <summary>
        /// checks all the board for naked singles, if found one sends it to naked_single method 
        /// and returns true otherwise returns flase.
        /// </summary>
        /// <returns>true or flase</returns>
        public bool solveNakedSingles()
        {
            bool flag = false;
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    if (board[row, col].value == 0)
                    {
                        initializing_posibilities(board[row, col]); // updating the cell's possibilities.
                        if (naked_single(board[row, col])) //if an naked single
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag; // if there was at keast one hiddden.
        }

        /// <summary>
        /// checks all the board for hidden singles, if found one sends changes his value and returns true, otherwise false.
        /// </summary>
        /// <returns>true or flase</returns>
        public bool solveHiiddenSingles()
        {
            bool flag = false;
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    if (board[row, col].value == 0) // empty cell
                    {
                        initializing_posibilities(board[row, col]); // updating the cell's possibilities.
                        if (hidden_single_in_row(board[row, col]) || hidden_single_in_col(board[row, col]) || hidden_single_in_box(board[row, col])) //if an hidden single
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag; //if there was at least one hidden.
        }

        /// <summary>
        /// the main solve method,using back track algorithm to solve the board.
        /// </summary>
        /// <returns>return true if the was able to solve the board and false otherwise.</returns>
        public bool solveBackTrack()
        {
            Board copy_board = new Board(board);
            if (howManyEmptyCells < (board.size * board.size) * 0.5) // if the board is full with zeros than don't try the human strategies.
                if (!humanStrategies(copy_board))
                    return false;
            Cell minCell = FindMinCell(); // getting the cell with the min possibilities in the board.
            if (minCell == null) // if the board is already solves, there are no empty cells.
                return true;
            foreach (int possibility in minCell.posibilities) // going through the min cell possibiliies.
            {
                this.howManyEmptyCells--; // the amount of empty cells decreased in 1
                board.changeCellInBoard(minCell, possibility); //changing the value in the board to the currant possibilitty.
                if (solveBackTrack()) // calling the fuction to the find the next cell, until returning true.
                {
                    return true;
                }
                // the possibilty isn't good, return the value to 0. 
                board.changeCellInBoard(minCell, 0); //changing the value in the board from the currant possibilitty to 0.
            }
            board = copy_board;
            this.howManyEmptyCells++; // the amount of empty cells encreased in 1
            return false; // if we tried all the possibilities and the board can't be solved.
        }

        /// <summary>
        /// return the amount of empty cells in the board.
        /// </summary>
        /// <returns></returns>
        private int amountOfEmptyCells()
        {
            int count = 0;
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    if (board[row, col].value == 0)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// the function uses human estrategies to solve the board, such as nakes aingle and hidden single.
        /// </summary>
        /// <param name="copy_board">replica of the board.</param>
        /// <returns>returns true if the board is legal after the change and false otherwise. </returns>
        public bool humanStrategies(Board copy_board)
        {
            bool hiddenFlag = true; // if there was at least 1 hidden single
            bool nakedFlag = true; // if there was at least 1 naked single
            while (hiddenFlag || nakedFlag)
            {
                try
                {
                    hiddenFlag = solveHiiddenSingles();
                    if (hiddenFlag)
                        this.howManyEmptyCells--; // the amount of empty cells decreased in 1
                    nakedFlag = solveNakedSingles();
                    if (nakedFlag)
                        this.howManyEmptyCells--; // the amount of empty cells decreased in 1
                }
                catch (UnsolveableBoardException) // if an illegal move was made than retrive the board and return flase.
                {
                    board = copy_board;
                    return false;
                }
            }
            return true; // everything went well.
        }
    }
}
