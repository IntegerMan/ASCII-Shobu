using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
    public class PassiveTurn
    {
        public bool TurnDone = false;
        private Player CurrentPlayer { get; }
        private Board[] MainBoards { get; }
        private Board CurrentBoard { get; set;}
        private Square StartSquare { get; set; }
        private Square EndSquare { get; set; }
        public Game CurrentGame { get; }
        private Move PassiveMove { get; set; }
        private bool InputIsValid = false;

        public PassiveTurn(Player currentPlayer, Board[] mainBoards, Game currentGame)
        {
            this.CurrentPlayer = currentPlayer;
            this.MainBoards = mainBoards;
            this.CurrentGame = currentGame;
        }

        public void GetUserInputForPassiveTurn()
        {
            Console.Write($"{CurrentPlayer}, select a board to make your move on:  ");
            string selectedBoardInput = Console.ReadLine();
            if (BoardLogic.BoardIsLegalForPassiveMove(selectedBoardInput) == false) { return; }

            this.CurrentBoard = this.MainBoards[int.Parse(selectedBoardInput) - 1];

            if (BoardLogic.BoardIsHomeBoard(CurrentPlayer, this.CurrentBoard.BoardNumber) == false) { return; }
            if (GetStartSquareFromUser() == false) { return; }
            if (GetEndSquareFromUser() == false) { return; }

            this.PassiveMove = new Move(this.StartSquare, this.EndSquare, this.CurrentBoard, this.CurrentPlayer.Name, true);
            InputIsValid = true;
        }

        private bool GetStartSquareFromUser() 
        {
            Console.Write($"{CurrentPlayer}, select a piece to move:  ");
            int startSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(Console.ReadLine());
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
            Console.Write("Select square to move to:  ");
            int endSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(Console.ReadLine());
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
            while (InputIsValid == false)
            {
                CurrentGame.Refresh();
                GetUserInputForPassiveTurn();
            }
            if (MoveLogic.MoveIsLegal(this.PassiveMove))
            {
                ExecutePassiveMove(this.PassiveMove);
                CurrentPlayer.LastMoveMade = PassiveMove;
                this.TurnDone = true;
            }
            else
            {
                Console.ReadLine();
                InputIsValid = false;
            }
        }

        public void ExecutePassiveMove(Move passiveMove)
        {
            passiveMove.StartSquare.HasO = false;
            passiveMove.StartSquare.HasX = false;

            Square transitionSquare = passiveMove.EndSquare;
            if (passiveMove.MoveIs2Spaces)
            {
                transitionSquare = passiveMove.BoardMoveIsOn.SquaresOnBoard[passiveMove.TransitionSquareIndex()];
            }
            if (passiveMove.GetIndexOfSquarePastMove() != -1)
            {
                Square squarePastMove = passiveMove.BoardMoveIsOn.SquaresOnBoard[passiveMove.GetIndexOfSquarePastMove()];
                if (passiveMove.EndSquare.HasX || transitionSquare.HasX)
                {
                    squarePastMove.HasX = true;
                }
                if (passiveMove.EndSquare.HasO || transitionSquare.HasO)
                {
                    squarePastMove.HasO = true;
                }
            }
            transitionSquare.HasO = false;
            transitionSquare.HasX = false;
            if (passiveMove.PlayerMakingMove == PlayerName.X)
            {
                passiveMove.EndSquare.HasX = true;
                passiveMove.EndSquare.HasO = false;
            }
            else
            {
                passiveMove.EndSquare.HasO = true;
                passiveMove.EndSquare.HasX = false;
            }
        }
    }
}
