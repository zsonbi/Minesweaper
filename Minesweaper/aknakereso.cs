using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweaper
{
    internal class aknakereso
    {
        private struct Cell
        {
            public CellType Type;
            public bool Visited;
        }

        private byte colCount;//The column count on the board
        private byte rowCount; //The row count on the board
        private byte MaxMine;
        private Cell[,] board;

        public aknakereso(byte rowCount, byte colCount, byte mineCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.MaxMine = mineCount;
            Generaloize();
        }

        private void Generaloize()
        {
            board = new Cell[rowCount, colCount];
            Random rnd = new Random();
            for (byte i = 0; i < MaxMine; i++)
            {
                byte random1 = (byte)rnd.Next(0, rowCount);
                byte random2 = (byte)rnd.Next(0, colCount);
                if (board[random1, random2].Type != CellType.Mine)
                {
                    board[random1, random2].Type = CellType.Mine;
                    Szamok(random1, random2);
                }
                else
                {
                    i--;
                }
            }
        }

        private void Szamok(byte row, byte col)
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

        public CellType GetCellType(byte row, byte col)
        {
            return board[row, col].Type;
        }

        public bool WasItVisited(byte row, byte col)
        {
            return board[row, col].Visited;
        }

        public void Visit(byte row, byte col)
        {
            board[row, col].Visited = true;
        }
    }
}