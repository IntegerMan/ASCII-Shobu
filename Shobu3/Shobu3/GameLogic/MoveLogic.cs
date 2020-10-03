using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    /// <summary>
    /// An assortment of methods to check if a move is legal.
    /// </summary>
    public static class MoveLogic
    {
        // Runs all checks and prints appropriate error message if check failed
        public static bool MoveIsLegal(Move move)
        {
            if (!MoveIsAStraightLine(move))
            {
                Console.WriteLine("Your piece must move in a straight line.  Press enter to continue.");
                return false;
            }
            if (!MoveIsLegalDistance(move))
            {
                Console.WriteLine("Your piece must move 1 or 2 squares.  Press enter to continue.");
                return false;
            }
            if (!MoveAvoidsOwnPieces(move))
            {
                Console.WriteLine("You may never push your own pieces.  Press enter to continue.");
                return false;
            }
            if (!MoveDoesNotPush2Pieces(move))
            {
                Console.WriteLine("You may not move through 2 pieces at once.  Press enter to continue.");
                return false;
            }
            if (!MoveDoesNotPushWhilePassive(move))
            {
                Console.WriteLine("You may not push other pieces with a passive move.  Press enter to continue.");
                return false;
            }
            return true;
        }

        private static bool MoveIsAStraightLine(Move move)
        {
            if (move.DistanceMovedOnX == 0 || move.DistanceMovedOnY == 0)
            {
                return true;
            }
            return move.DistanceMovedOnY == move.DistanceMovedOnX;
        }

        private static bool MoveIsLegalDistance(Move move)
        {
            if (move.DistanceMovedOnX == 0 && move.DistanceMovedOnY == 0)
            {
                return false;
            }
            return (move.DistanceMovedOnX < 3 && move.DistanceMovedOnY < 3);
        }

        private static bool MoveAvoidsOwnPieces(Move move)
        {
            if (move.PlayerMakingMove == PlayerName.X)
            {
                if (move.EndSquare.HasX)
                {
                    return false;
                }
                if (move.MoveIs2Spaces)
                {
                    if (move.BoardMoveIsOn.SquaresOnBoard[move.TransitionSquareIndex()].HasX)
                    {
                        return false;
                    }
                }
            }
            if (move.PlayerMakingMove == PlayerName.O)
            {
                if (move.EndSquare.HasO)
                {
                    return false;
                }
                if (move.MoveIs2Spaces)
                {
                    if (move.BoardMoveIsOn.SquaresOnBoard[move.TransitionSquareIndex()].HasO)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool MoveDoesNotPush2Pieces(Move move)
        {
            int piecesInPath = 0;
            int squarePastMoveIndex = move.GetIndexOfSquarePastMove();

            if (move.EndSquare.HasO || move.EndSquare.HasX)
            {
                piecesInPath++;
            }
            if (squarePastMoveIndex != -1)
            {
                Square squarePastMove = move.BoardMoveIsOn.SquaresOnBoard[squarePastMoveIndex];
                if (squarePastMove.HasO || squarePastMove.HasX)
                {
                    piecesInPath++;
                }
            }
            if (move.MoveIs2Spaces)
            {
                Square transitionSquare = move.BoardMoveIsOn.SquaresOnBoard[move.TransitionSquareIndex()];
                if (transitionSquare.HasO || transitionSquare.HasX)
                {
                    piecesInPath++;
                }
            }
            return piecesInPath < 2;
        }

        private static bool MoveDoesNotPushWhilePassive(Move move)
        {
            if (!move.IsPassive)
            {
                return true;
            }
            if (move.EndSquare.HasO || move.EndSquare.HasX)
            {
                return false;
            }
            if (move.MoveIs2Spaces)
            {
                Square transitionSquare = move.BoardMoveIsOn.SquaresOnBoard[move.TransitionSquareIndex()];
                if (transitionSquare.HasX || transitionSquare.HasO)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
