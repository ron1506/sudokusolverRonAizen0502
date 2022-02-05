using System;
using System.Collections.Generic;
using System.Text;

namespace sudokusolverRonAizen
{
    public interface IOutputer
    {
        void Output(Board board); // returns the solved sudoko board.
    }
}
