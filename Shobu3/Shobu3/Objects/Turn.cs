using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
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


        public Turn(Player currentPlayer, Board[] mainBoards, Game currentGame)
        {
            this.CurrentPlayer = currentPlayer;
            this.MainBoards = mainBoards;
            this.CurrentGame = currentGame;
            ExecutePassiveTurn();
        }

        public bool GetUserInputForTurn()
        {
            Console.Write($"{CurrentPlayer}, select a board to make your {this.currentTurnType} move on:  ");
            string selectedBoardInput = Console.ReadLine();
            if (BoardLogic.IsValidBoard(selectedBoardInput) == false) { return false; }
            this.CurrentBoard = this.MainBoards[int.Parse(selectedBoardInput) - 1];

            if (this.TurnIsPassive && BoardLogic.BoardIsHomeBoard(CurrentPlayer, this.CurrentBoard.BoardNumber) == false) { return false; }
            if (!this.TurnIsPassive) {
                if (BoardLogic.BoardIsLegalForAggressiveMove(CurrentPlayer.LastMoveMade, CurrentBoard.BoardNumber) == false ) { return false; }
            }

            if (GetStartSquareFromUser() == false) { return false; }
            if (GetEndSquareFromUser() == false) { return false; }

            this.CurrentMove = new Move(this.StartSquare, this.EndSquare, this.CurrentBoard, this.CurrentPlayer.Name, (TurnIsPassive));
            return true;
        }

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

        public void ExecutePassiveTurn()
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
                    ExecuteAggressiveTurn();
                }
                else
                {
                    Console.ReadLine();
                    PassiveTurnDone = false;
                }
                CurrentGame.Refresh();
            }
        }

        public void ExecuteAggressiveTurn()
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
                    Console.ReadLine();
                }
            }
        }

        public void ExecuteCurrentMove(Move currentMove)
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
