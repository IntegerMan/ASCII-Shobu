using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
    /// <summary>
    /// Represents a full turn and tracks relevant information through its duration
    /// </summary>
    public class Turn
    {
        public bool TurnDone = false;
        private Player CurrentPlayer { get; }
        private Board[] MainBoards { get; }
        private Board CurrentBoard { get; set;}
        private Square StartSquare { get; set; }
        private Square EndSquare { get; set; }
        public Game CurrentGame { get; }
        private Move CurrentMove { get; set; }
        private TurnType currentTurnType = TurnType.Passive;
        private bool TurnIsPassive = true;
        private bool PassiveTurnDone = false;
        /// <summary>
        /// Runs a turn, checks mid-turn for the end game condition of no legal
        /// aggressive moves remaining after passive move
        /// </summary>
        public Turn(Game currentGame)
        {
            this.CurrentPlayer = currentGame.currentPlayer;
            this.MainBoards = currentGame.mainBoards;
            this.CurrentGame = currentGame;
            ExecutePassiveTurn();
            if (EndGame.NoLegalAggressiveMove(currentGame))
            {
                Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                Console.WriteLine("There are no legal aggressive moves based on that passive move.");
                Console.WriteLine(EndGame.DisplayWinMessageForOpponent(CurrentPlayer));
                currentGame.GameIsDone = true;
            }
            else
            {
                ExecuteAggressiveTurn();
            }
        }
        /// <summary>
        /// Collects input from user for their next move.  Returns
        /// false if input is invalid or illegal.  Creates and stores
        /// move as CurrentMove if legal.
        /// </summary>
        public bool GetUserInputForTurn()
        {
            if (!GetBoardFromUser()) { return false; }
            if (!GetStartSquareFromUser()) { return false; }
            if (!GetEndSquareFromUser()) { return false; }

            this.CurrentMove = new Move(this.StartSquare, this.EndSquare, this.CurrentBoard, this.CurrentPlayer.Name, (TurnIsPassive));
            return true;
        }
        /// <summary>
        /// Gets board selection from user.  If illegal, returns false. If legal, assigns as CurrentBoard.
        /// </summary>
        private bool GetBoardFromUser()
        {
            Console.Write($"{CurrentPlayer}, select a board to make your {this.currentTurnType} move on (or type \"rules\" to see rules):  ");
            string selectedBoardInput = Console.ReadLine();
            if (BoardLogic.IsValidBoard(selectedBoardInput) == false) { return false; }
            this.CurrentBoard = this.MainBoards[int.Parse(selectedBoardInput) - 1];

            if (this.TurnIsPassive && BoardLogic.BoardIsHomeBoard(CurrentPlayer, this.CurrentBoard.BoardNumber) == false)
            {
                Console.WriteLine("Passive move must be made on a home board. Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            if (!this.TurnIsPassive)
            {
                if (BoardLogic.BoardIsLegalForAggressiveMove(CurrentPlayer.LastMoveMade, CurrentBoard.BoardNumber) == false)
                {
                    Console.WriteLine("Aggressive move must be made on a board of different color than your passive move.");
                    Console.ReadLine();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Gets start square from user.  Returns false if illegal. Assigns to StartSquare if legal.
        /// </summary>
        private bool GetStartSquareFromUser() 
        {
            Regex numLetterCheck = new Regex(@"[a-d][1-4]");
            Console.Write($"{CurrentPlayer}, select a piece to move:  ");
            string userInput = Console.ReadLine();
            if (!numLetterCheck.IsMatch(userInput))
            {
                Console.WriteLine("Not a valid square.  Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            int startSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(userInput);
            if (startSquareInput == -1)
            {
                Console.WriteLine("Not a valid square.  Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            this.StartSquare = this.CurrentBoard.SquaresOnBoard[startSquareInput];
            if (BoardLogic.SquareHasOwnPiece(this.StartSquare, CurrentPlayer.Name) == false)
            {
                Console.WriteLine("You have no piece in that square.  Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets end square from user.  Returns false if illegal. Assigns to EndSquare if legal.
        /// </summary>
        private bool GetEndSquareFromUser()
        {
            Regex numLetterCheck = new Regex(@"[a-d][1-4]");
            Console.Write("Select square to move to:  ");
            string userInput = Console.ReadLine();
            if (!numLetterCheck.IsMatch(userInput))
            {
                Console.WriteLine("Not a valid square.  Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            int endSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(userInput);
            if (endSquareInput == -1)
            {
                Console.WriteLine("Not a valid square.  Press enter to continue.");
                Console.ReadLine();
                return false;
            }
            this.EndSquare = this.CurrentBoard.SquaresOnBoard[endSquareInput];
            return true;
        }
        /// <summary>
        /// Collects input from user for passive turn, ensures legality, then executes move.
        /// Also updates Turn properties to reflect upcoming aggressive turn.
        /// </summary>
        private void ExecutePassiveTurn()
        {
            while (!PassiveTurnDone)
            {
                while (!GetUserInputForTurn())
                {
                    CurrentGame.Refresh();
                }
                if (MoveLogic.MoveIsLegal(this.CurrentMove))
                {
                    ExecuteCurrentMove(this.CurrentMove);
                    CurrentPlayer.LastMoveMade = CurrentMove;
                    TurnIsPassive = false;
                    PassiveTurnDone = true;
                    this.currentTurnType = TurnType.Aggressive;
                }
                else
                {
                    Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
                    Console.ReadLine();
                    PassiveTurnDone = false;
                }
                CurrentGame.Refresh();
            }
        }
        /// <summary>
        /// Collects input from user for aggressive turn, ensures legality, then executes move.
        /// </summary>
        private void ExecuteAggressiveTurn()
        {
            while (TurnDone == false)
            {
                CurrentGame.Refresh();
                Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                while (!GetUserInputForTurn())
                {
                    CurrentGame.Refresh();
                    Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                }
                if (MoveLogic.MoveIsLegal(this.CurrentMove) && MoveLogic.MatchesPassiveMoveWhileAggressive(this.CurrentMove, this.CurrentPlayer.LastMoveMade))
                {
                    ExecuteCurrentMove(this.CurrentMove);
                    TurnDone = true;
                }
                else
                {
                    Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Called after move is verified as legal.  Executes move on applicable board.
        /// </summary>
        /// <param name="currentMove"></param>
        private void ExecuteCurrentMove(Move currentMove)
        {
            currentMove.StartSquare.HasO = false;
            currentMove.StartSquare.HasX = false;

            Square transitionSquare = currentMove.EndSquare;
            if (currentMove.MoveIs2Spaces)
            {
                transitionSquare = currentMove.BoardMoveIsOn.SquaresOnBoard[currentMove.TransitionSquareIndex()];
            }
            if (currentMove.GetIndexOfSquarePastMove() != -1)
            {
                Square squarePastMove = currentMove.BoardMoveIsOn.SquaresOnBoard[currentMove.GetIndexOfSquarePastMove()];
                if (currentMove.EndSquare.HasX || transitionSquare.HasX)
                {
                    squarePastMove.HasX = true;
                }
                if (currentMove.EndSquare.HasO || transitionSquare.HasO)
                {
                    squarePastMove.HasO = true;
                }
            }
            transitionSquare.HasO = false;
            transitionSquare.HasX = false;
            if (currentMove.PlayerMakingMove == PlayerName.X)
            {
                currentMove.EndSquare.HasX = true;
                currentMove.EndSquare.HasO = false;
            }
            else
            {
                currentMove.EndSquare.HasO = true;
                currentMove.EndSquare.HasX = false;
            }
        }
    }
}
