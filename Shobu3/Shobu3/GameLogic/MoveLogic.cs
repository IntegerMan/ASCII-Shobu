using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    /// <summary>
    /// An assortment of methods to check if a move is legal.
    /// </summary>
    public static class MoveLogic
    {
        // Runs all checks and assigns appropriate broken rule to move if check failed
        public static bool MoveIsLegal(Move move)
        {
            if (!MoveIsAStraightLine(move))
            {
                move.BrokenRule = MoveRules.StraightLine;
                return false;
            }
            if (!MoveIsLegalDistance(move))
            {
                move.BrokenRule = MoveRules.LegalDistance;
                return false;
            }
            if (!MoveAvoidsOwnPieces(move))
            {
                move.BrokenRule = MoveRules.AvoidsOwnPieces;
                return false;
            }
            if (!MoveDoesNotPush2Pieces(move))
            {
                move.BrokenRule = MoveRules.PushesLessThan2Pieces;
                return false;
            }
            if (!MoveDoesNotPushWhilePassive(move))
            {
                move.BrokenRule = MoveRules.DoesNotPushWhilePassive;
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

        public static bool MatchesPassiveMoveWhileAggressive(Move aggressiveMove, Move passiveMove)
        {
            int passiveStartX = passiveMove.StartSquare.XCoordinate;
            int passiveStartY = passiveMove.StartSquare.YCoordinate;
            int passiveEndX = passiveMove.EndSquare.XCoordinate;
            int passiveEndY = passiveMove.EndSquare.YCoordinate;
            int aggressiveStartX = aggressiveMove.StartSquare.XCoordinate;
            int aggressiveStartY = aggressiveMove.StartSquare.YCoordinate;
            int aggressiveEndX = aggressiveMove.EndSquare.XCoordinate;
            int aggressiveEndY = aggressiveMove.EndSquare.YCoordinate;

            if (passiveStartX - passiveEndX == aggressiveStartX - aggressiveEndX)
            {
                if (passiveStartY - passiveEndY == aggressiveStartY - aggressiveEndY)
                {
                    return true;
                }
            }
            aggressiveMove.BrokenRule = MoveRules.MatchesPassiveMoveWhileAggressive;
            return false;
        }

        public static string PrintErrorMessage(Move move)
        {
            return move.BrokenRule switch
            {
                MoveRules.StraightLine => "Your piece must move in a straight line.",
                MoveRules.LegalDistance => "Your piece must move 1 or 2 squares.",
                MoveRules.AvoidsOwnPieces => "You may never push your own pieces.",
                MoveRules.PushesLessThan2Pieces => "You may never push more than 1 piece.",
                MoveRules.MatchesPassiveMoveWhileAggressive => "Aggressive move must match direction and distance of Passive move.",
                _ => "Legal move.",
            };
        }
    }
}
