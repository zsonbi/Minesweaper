using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweaper
{
    /// <summary>
    /// Creates a new instance of the game
    /// </summary>
    internal class MinesweaperGame
    {
        /// <summary>
        /// A single cell
        /// </summary>
        private struct Cell
        {
            /// <summary>
            /// The type of the cell
            /// </summary>
            public CellType Type;

            /// <summary>
            /// Has it been visited
            /// </summary>
            public bool Visited;
        }

        private byte colCount;//The column count on the board
        private byte rowCount; //The row count on the board
        private short MaxMine; //Maximum how many mines can the board have
        private Cell[,] board; //The board stored in a 2d array

        //--------------------------------------------------------------
        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="rowCount">The number of rows it will have</param>
        /// <param name="colCount">The number of columns it will have</param>
        /// <param name="mineCount">The number of mines will the game have</param>
        public MinesweaperGame(byte rowCount, byte colCount, short mineCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.MaxMine = mineCount;
            if (rowCount * colCount < mineCount)
            {
                this.MaxMine = (short)(rowCount * colCount * 0.9f);
            }

            GenerateBoard();
        }

        //--------------------------------------------------------------------------
        //Generates a new board
        private void GenerateBoard()
        {
            board = new Cell[rowCount, colCount];
            Random rnd = new Random();
            for (byte i = 0; i < MaxMine; i++)
            {
                byte rowIndex = (byte)rnd.Next(0, rowCount);
                byte colIndex = (byte)rnd.Next(0, colCount);
                if (board[rowIndex, colIndex].Type != CellType.Mine)
                {
                    board[rowIndex, colIndex].Type = CellType.Mine;
                    IncrementNearbyCells(rowIndex, colIndex);
                }
                else
                {
                    i--;
                }
            }
        }

        //--------------------------------------------------------------------
        //Increment the nearby (8 cell) value by 1 if it isn't a mine
        private void IncrementNearbyCells(byte row, byte col)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                if (i < 0 || i >= rowCount)
                {
                    continue;
                }
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (j < 0 || j >= colCount)
                    {
                        continue;
                    }
                    if (board[i, j].Type != CellType.Mine)
                    {
                        board[i, j].Type++;
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Returns the type of the indexed cell
        /// </summary>
        /// <param name="row">The row of the cell on the board</param>
        /// <param name="col">The column of the cell on the board</param>
        /// <returns>The type of the cell</returns>
        public CellType GetCellType(byte row, byte col)
        {
            return board[row, col].Type;
        }

        //---------------------------------------------------------------------------
        /// <summary>
        /// Returns a bool whether the cell has been visited
        /// </summary>
        /// <param name="row">The row of the cell on the board</param>
        /// <param name="col">The column of the cell on the board</param>
        /// <returns>true - if it was visited, false - if it wasn't visited</returns>
        public bool WasItVisited(byte row, byte col)
        {
            return board[row, col].Visited;
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Set the visited property of the selected cell to true
        /// </summary>
        /// <param name="row">The row of the cell on the board</param>
        /// <param name="col">The column of the cell on the board</param>
        public void Visit(byte row, byte col)
        {
            board[row, col].Visited = true;
        }
    }
}