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
            return Conversion.ConvertXandYToBoardIndex(x, y);
        }

        // Returns index of square past move or -1 if at edge of board
        public int GetIndexOfSquarePastMove()
        {
            int squarePastMoveX = 0;
            int squarePastMoveY = 0;

            if (DistanceMovedOnX == 0)
            {
                squarePastMoveX = this.StartSquare.XCoordinate;
            }
            else if (StartSquare.XCoordinate > EndSquare.XCoordinate)
            {
                squarePastMoveX = this.EndSquare.XCoordinate - 1;
            }
            else if (StartSquare.XCoordinate < EndSquare.XCoordinate)
            {
                squarePastMoveX = this.EndSquare.XCoordinate + 1;
            }

            if (squarePastMoveX > 4 || squarePastMoveX < 1)
            {
                return -1;
            }

            if (DistanceMovedOnY == 0)
            {
                squarePastMoveY = this.StartSquare.YCoordinate;
            }
            else if (StartSquare.YCoordinate > EndSquare.YCoordinate)
            {
                squarePastMoveY = this.EndSquare.YCoordinate - 1;
            }
            else if (StartSquare.YCoordinate < EndSquare.YCoordinate)
            {
                squarePastMoveY = this.EndSquare.YCoordinate + 1;
            }

            if (squarePastMoveY > 4 || squarePastMoveY < 1)
            {
                return -1;
            }

            return (squarePastMoveX - 1) + ((squarePastMoveY - 1) * 4);
        }

        public override string ToString()
        {
            return $"Player {this.PlayerMakingMove} moved from {this.StartSquare} to {this.EndSquare} on {this.BoardMoveIsOn}.";
        }
    }
}
