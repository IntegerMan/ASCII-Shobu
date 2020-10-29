using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    /// <summary>
    /// Provides methods for determining winner and if an
    /// end game condition is met.
    /// </summary>
    public static class EndGame
    {
        /// <summary>
        /// Checks boards to see if a player's pieces have been eliminated.
        /// </summary>
        public static bool BoardHasOnlyXsOrOs(Board[] boards)
        {
            foreach (Board board in boards)
            {
                if (!board.HasOs || !board.HasXs)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Returns player that triggered winning end game condition.
        /// </summary>
        public static PlayerName DetermineWinner(Board[] boards)
        {
            foreach (Board board in boards)
            {
                if (!board.HasOs)
                {
                    return PlayerName.X;
                }
                else if (!board.HasXs)
                {
                    return PlayerName.O;
                }
            }
            Console.WriteLine("ERROR OCCURRED.  DETERMINEWINNER OR BOARDHASONLYXSOROS FAULTY");
            return PlayerName.X;
        }
        /// <summary>
        /// Searches legal boards for at least 1 legal aggressive move.
        /// </summary>
        public static bool NoLegalAggressiveMove(Game game)
        {
            foreach (Board board in game.mainBoards)
            {
                if (!BoardLogic.BoardIsLegalForAggressiveMove(game.currentPlayer.LastMoveMade, board.BoardNumber))
                {
                    continue;
                }
                if (board.HasOs && game.currentPlayer.Name == PlayerName.O || board.HasXs && game.currentPlayer.Name == PlayerName.X)
                {
                    foreach (Square square in board.SquaresOnBoard)
                    {
                        if ((square.HasO && game.currentPlayer.Name == PlayerName.O)
                            ||
                            (square.HasX && game.currentPlayer.Name == PlayerName.X))
                        {
                            if (SquareHasLegalAggressiveMove(game, square, board))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks square as starting point for possible aggressive move based on last passive move.
        /// </summary>
        private static bool SquareHasLegalAggressiveMove(Game game, Square startSquare, Board board)
        {
            Square endSquare = GetEndSquareFromPassiveMove(game, startSquare, board);
            if (endSquare == null) { return false; }
            Move testMove = new Move(startSquare, endSquare, board, game.currentPlayer.Name, false);
            return MoveLogic.MoveIsLegal(testMove);
        }
        /// <summary>
        /// Selects endSquare for aggressive move based on last passive Move.
        /// Returns null if not on board.
        /// </summary>
        private static Square GetEndSquareFromPassiveMove(Game game, Square startSquare, Board board)
        {
            int passiveStartX = game.currentPlayer.LastMoveMade.StartSquare.XCoordinate;
            int passiveStartY = game.currentPlayer.LastMoveMade.StartSquare.YCoordinate;
            int passiveEndX = game.currentPlayer.LastMoveMade.EndSquare.XCoordinate;
            int passiveEndY = game.currentPlayer.LastMoveMade.EndSquare.YCoordinate;
            int aggressiveStartX = startSquare.XCoordinate;
            int aggressiveStartY = startSquare.YCoordinate;
            int aggressiveEndX = aggressiveStartX - (passiveStartX - passiveEndX);
            int aggressiveEndY = aggressiveStartY - (passiveStartY - passiveEndY);
            
            if ((aggressiveEndY > 0 && aggressiveEndY < 5)
                &&
                (aggressiveEndX > 0 && aggressiveEndX < 5))
            {
            int resultIndex = Conversion.ConvertXAndYToBoardIndex(aggressiveEndX, aggressiveEndY);
            return board.SquaresOnBoard[resultIndex];
            }
            return null;
        }
        /// <summary>
        /// Prints message declaring opposing player as winner.
        /// </summary>
        public static string DisplayWinMessageForOpponent(Player loser)
        {
            string winner = "";
            if (loser.Name == PlayerName.X)
            {
                winner = "O";
            }
            else { winner = "X"; }
            return $"Player {winner} is the winner!";
        }
    }
}
