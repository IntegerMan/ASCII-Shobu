using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    public static class Conversion
    {
        // Converts a Square to its index number on a Board
        public static int ConvertSquareToBoardIndex(Square square)
        {
            int x = square.XCoordinate;
            int y = square.YCoordinate;
            return (x - 1) + ((y - 1) * Board.BoardSize);
        }

        // Converts x and y to square index on a board
        public static int ConvertXAndYToBoardIndex(int x, int y)
        {
            return (x - 1) + ((y - 1) * Board.BoardSize);
        }

        // Converts the letter+number input from user into the
        // index number of the square.
        public static int ConvertLetterNumInputToBoardIndex(string input)
        {
            int result = int.Parse(input[1].ToString()) - 1;
            switch (input[0].ToString().ToLower())
            {
                case "a":
                    break;
                case "b":
                    result += Board.BoardSize;
                    break;
                case "c":
                    result += 8;
                    break;
                case "d":
                    result += 12;
                    break;
                default:
                    return -1;
            }
            return result;
        }
    }
}
