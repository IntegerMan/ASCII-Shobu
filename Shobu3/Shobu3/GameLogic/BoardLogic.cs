using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    /// <summary>
    /// Methods to check that board rules are enforced.
    /// </summary>
    public static class BoardLogic
    {
        // Checks if a board is a homeboard of the player.
        public static bool BoardIsHomeBoard(Player player, int boardNum)
        {
            foreach (int num in player.HomeBoards)
            {
                if (num == boardNum)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if board is of a different color
        // than the previous passive move's board
        public static bool BoardIsLegalForAggressiveMove(Move passiveMove, int boardNum)
        {
            if (!(passiveMove.BoardMoveIsOn.BoardNumber % 2 != boardNum % 2))
            {
                return false;
            }
            return true;
        }

        // Checks that user input is a board, or a
        // request to see rules.
        public static bool IsValidBoard(string selectedBoardInput)
        {
            switch (selectedBoardInput)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    break;
                case "rules":
                    Console.Clear();
                    Console.WriteLine(Game.rules);
                    Console.WriteLine("\nPress enter to resume.");
                    Console.ReadLine();
                    return false;
                default:
                    Console.WriteLine("Not a valid board. Press enter to continue.");
                    Console.ReadLine();
                    return false;
            }
            return true;
        }

        // Checks if square contains player's own piece.
        public static bool SquareHasOwnPiece(Square square, PlayerName player)
        {
            if (square.HasO && player == PlayerName.O)
            {
                return true;
            }
            if (square.HasX && player == PlayerName.X)
            {
                return true;
            }
            return false;
        }
    }
}
