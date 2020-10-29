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
            return (x - 1) + ((y - 1) * 4);
        }

        // Converts x and y to square index on a board
        public static int ConvertXAndYToBoardIndex(int x, int y)
        {
            return (x - 1) + ((y - 1) * 4);
        }

        // Converts the letter+number input from user into the
        // index number of the square.
        public static int ConvertLetterNumInputToBoardIndex(string input)
        {
            int result = int.Parse(input[1].ToString()) - 1;
            switch (input[0])
            {
                case 'a':
                case 'A':
                    break;
                case 'b':
                case 'B':
                    result += 4;
                    break;
                case 'c':
                case 'C':
                    result += 8;
                    break;
                case 'd':
                case 'D':
                    result += 12;
                    break;
                default:
                    return -1;
            }
            return result;
        }
    }
}
