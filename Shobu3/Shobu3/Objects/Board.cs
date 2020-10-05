using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;

namespace Shobu3
{
    /// <summary>
    /// Represents a single game board.  Holds squares, own number,
    /// and tracks if there are Xs and/or Os present.
    /// </summary>
    public class Board
    {
        public Square[] SquaresOnBoard { get; }
        public int BoardNumber { get; }
        public bool HasXs
        {
            get
            {
                bool foundX = false;
                foreach (Square square in SquaresOnBoard)
                {
                    if (square.HasX == true)
                    {
                        foundX = true;
                        break;
                    }
                }
                return foundX;
            }
        }

        public bool HasOs
        {
            get
            {
                bool foundO = false;
                foreach (Square square in SquaresOnBoard)
                {
                    if (square.HasO == true)
                    {
                        foundO = true;
                        break;
                    }
                }
                return foundO;
            }

        }

        public Board(int boardNumber)
        {
            this.BoardNumber = boardNumber;
            this.SquaresOnBoard = new Square[16];
            int squareIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.SquaresOnBoard[squareIndex] = new Square(j+1, i+1);
                    squareIndex++;
                }
            }
            for (int i = 0; i < 4; i ++)
            {
                this.SquaresOnBoard[i].HasX = true;
                this.SquaresOnBoard[12 + i].HasO = true;
            }
        }

        public override string ToString()
        {
            return "Board " + this.BoardNumber;
        }

        public string GetRowAsString(int row)
        {
            string result = "|";
            for (int i = 0; i < 4; i++)
            {
                int squareIndex = ((row - 1) * 4) + i;
                if (this.SquaresOnBoard[squareIndex].HasX)
                {
                    result += "X";
                }
                else if (this.SquaresOnBoard[squareIndex].HasO)
                {
                    result += "O";
                }
                else
                {
                    result += "-";
                }
                result += "|";
            }
            return result;
        }
    }
}
