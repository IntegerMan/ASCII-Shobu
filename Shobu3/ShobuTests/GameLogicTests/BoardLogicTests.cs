using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shobu3;
using Shobu3.GameLogic;
using Shobu3.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShobuTests.GameLogicTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class BoardLogicTests
    {
        [TestMethod]
        public void BoardIsHomeBoardTest()
        {
            // Arrange
            Board testBoard = new Board(1);
            Player testPlayer = new Player(PlayerName.X, new int[] { 1, 2 });
            // Act
            bool positiveResult = BoardLogic.BoardIsHomeBoard(testPlayer, 1);
            bool negativeResult = BoardLogic.BoardIsHomeBoard(testPlayer, 3);
            // Assert
            Assert.IsTrue(positiveResult);
            Assert.IsFalse(negativeResult);
        }
        [TestMethod]
        public void BoardIsLegalForAggressiveMoveTest()
        {
            // Arrange
            Board testBoard = new Board(1);
            Move testPassive = new Move(testBoard.SquaresOnBoard[0], testBoard.SquaresOnBoard[1], testBoard, PlayerName.X, true);
            // Act
            bool positiveResult = BoardLogic.BoardIsLegalForAggressiveMove(testPassive, 2);
            bool negativeResult = BoardLogic.BoardIsLegalForAggressiveMove(testPassive, 3);
            // Assert
            Assert.IsTrue(positiveResult);
            Assert.IsFalse(negativeResult);
        }

        [TestMethod]
        public void SquareHasOwnPieceTest()
        {
            // Arrange
            Board testBoard = new Board(1);
            // Act
            bool positiveResult = BoardLogic.SquareHasOwnPiece(testBoard.SquaresOnBoard[0], PlayerName.X);
            bool negativeResult = BoardLogic.SquareHasOwnPiece(testBoard.SquaresOnBoard[0], PlayerName.O);
            // Assert
            Assert.IsTrue(positiveResult);
            Assert.IsFalse(negativeResult);
        }
    }

}
