using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweaper
{
    class aknakereso
    {
        struct Cell
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
                }
                else {
                    i--;
                }

            }


        }

    }
}
