using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;
using Shobu3.Objects;
using Shobu3;

namespace ShobuTests.GameLogicTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MoveLogicTests
    {
        [TestMethod]
        [DataRow(true, 4)]
        [DataRow(true, 8)]
        [DataRow(true, 10)]
        [DataRow(false, 3)]
        [DataRow(false, 6)]
        [DataRow(false, 1)]
        [DataRow(false, 12)]
        public void MoveIsLegalTest(bool expected, int endIndex)
        {
            // Arrange
            Board testBoard = new Board(1);
            Move testMove = new Move(testBoard.SquaresOnBoard[0], testBoard.SquaresOnBoard[endIndex], testBoard, PlayerName.X, true);
            // Act
            bool result = MoveLogic.MoveIsLegal(testMove);
            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void DoesNotPushTwoPiecesTest()
        {
            // Arrange
            Game testGame = new Game();
            Move testMove = new Move(testGame.mainBoards[0].SquaresOnBoard[0], testGame.mainBoards[0].SquaresOnBoard[8], testGame.mainBoards[0], PlayerName.X, true);
            testGame.mainBoards[0].SquaresOnBoard[4].HasO = true;
            // Act
            bool result = MoveLogic.MoveIsLegal(testMove);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        [DataRow(5, false)]
        [DataRow(8, true)]
        [DataRow(4, false)]
        public void MatchesPassiveMoveWhileAggressiveTest(int endIndex, bool expected)
        {
            // Arrange
            Game testGame = new Game();
            testGame.currentPlayer.LastMoveMade = new Move(testGame.mainBoards[0].SquaresOnBoard[0], testGame.mainBoards[0].SquaresOnBoard[8], testGame.mainBoards[0], PlayerName.X, true);
            Move testMove = new Move(testGame.mainBoards[1].SquaresOnBoard[0], testGame.mainBoards[1].SquaresOnBoard[endIndex], testGame.mainBoards[1], PlayerName.X, false);
            // Act
            bool result = MoveLogic.MatchesPassiveMoveWhileAggressive(testMove, testGame.currentPlayer.LastMoveMade);
            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
