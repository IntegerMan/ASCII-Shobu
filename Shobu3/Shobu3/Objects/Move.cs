using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
    /// <summary>
    /// Stores the data required for a move:
    /// Start, end, distance on each axis, passive/aggressive,
    /// broken rule (if applicable), and if move is 2 spaces.
    /// </summary>
    public class Move
    {
        public Move(Square start, Square end, Board board, PlayerName player, bool isPassive)
        {
            this.StartSquare = start;
            this.EndSquare = end;
            this.BoardMoveIsOn = board;
            this.PlayerMakingMove = player;
            this.IsPassive = isPassive;
        }
        public MoveRules BrokenRule { get; set; }
        public PlayerName PlayerMakingMove { get; }
        public Square StartSquare { get; }
        public Square EndSquare { get; }
        public Board BoardMoveIsOn { get; }
        public bool IsPassive { get; }
        public int DistanceMovedOnX
        {
            get
            {
                return Math.Abs(StartSquare.XCoordinate - EndSquare.XCoordinate);
            }
        }
        public int DistanceMovedOnY
        {
            get
            {
                return Math.Abs(StartSquare.YCoordinate - EndSquare.YCoordinate);
            }
        }
        public bool MoveIs2Spaces 
        { 
            get
            {
                return ((DistanceMovedOnX == 2) || (DistanceMovedOnY == 2));
            }
        }

        // Returns index of the square between start and end if the move travelled
        // 2 squares.  Else, returns -1.
        public int TransitionSquareIndex()
        {
            if (!this.MoveIs2Spaces)
            {
                return -1;
            }
            int x = this.StartSquare.XCoordinate;
            int y = this.StartSquare.YCoordinate;

            if (this.DistanceMovedOnX == 2)
            {
                x = (this.StartSquare.XCoordinate + this.EndSquare.XCoordinate) / 2;
            }
            if (this.DistanceMovedOnY == 2)
            {
                y = (this.StartSquare.YCoordinate + this.EndSquare.YCoordinate) / 2;
            }
            return Conversion.ConvertXAndYToBoardIndex(x, y);
        }

        // Returns index of square past move or -1 if at edge of board
        public int GetIndexOfSquarePastMove()
        {
            int squarePastMoveX = GetSquarePastMoveOnSingleAxis(DistanceMovedOnX, StartSquare.XCoordinate, EndSquare.XCoordinate);
            if (squarePastMoveX > 4 || squarePastMoveX < 1)
            {
                return -1;
            }

            int squarePastMoveY = GetSquarePastMoveOnSingleAxis(DistanceMovedOnY, StartSquare.YCoordinate, EndSquare.YCoordinate);
            if (squarePastMoveY > 4 || squarePastMoveY < 1)
            {
                return -1;
            }
            return Conversion.ConvertXAndYToBoardIndex(squarePastMoveX, squarePastMoveY);
        }
        /// <summary>
        /// Returns coordinate of square past the given single-axis movement
        /// </summary>
        private int GetSquarePastMoveOnSingleAxis(int distance, int axisStart, int axisEnd)
        {
            if (distance == 0)
            {
                return axisStart;
            }
            else if (axisStart > axisEnd)
            {
                return axisEnd - 1;
            }
            return axisEnd + 1;
        }

        public override string ToString()
        {
            return $"Player {this.PlayerMakingMove} moved from {this.StartSquare} to {this.EndSquare} on {this.BoardMoveIsOn}.";
        }
    }
}
