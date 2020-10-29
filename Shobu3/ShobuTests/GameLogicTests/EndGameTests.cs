using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;
using Shobu3.Objects;

namespace ShobuTests.GameLogicTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class EndGameTests
    {
        [TestMethod]
        public void BoardHasOnlyXsOrOs()
        {
            // Arrange
            Game testGame = new Game();
            // Act
            bool negativeResult = EndGame.BoardHasOnlyXsOrOs(testGame.mainBoards);
            for (int i = 0; i < 4; i++)
            {
                testGame.mainBoards[0].SquaresOnBoard[i].HasX = false;
            }
            bool positiveResult = EndGame.BoardHasOnlyXsOrOs(testGame.mainBoards);
            // Assert
            Assert.IsFalse(negativeResult);
            Assert.IsTrue(positiveResult);
        }
        [TestMethod]
        public void DetermineWinnerTest()
        {
            // Arrange
            Game testGame = new Game();
            for (int i = 0; i < 4; i++)
            {
                testGame.mainBoards[0].SquaresOnBoard[i].HasX = false;
            }
            // Act
            PlayerName result = EndGame.DetermineWinner(testGame.mainBoards);
            // Assert
            Assert.AreEqual(PlayerName.O, result);
        }
        [TestMethod]
        public void NoLegalAggressiveMoveTest()
        {
            // Arrange
            Game testGame = new Game();
            Move testMove = new Move(testGame.mainBoards[0].SquaresOnBoard[4], testGame.mainBoards[0].SquaresOnBoard[6], testGame.mainBoards[0], PlayerName.X, true);
            testGame.currentPlayer.LastMoveMade = testMove;
            // Act
            bool positiveResult = EndGame.NoLegalAggressiveMove(testGame);
            testGame.mainBoards[1].SquaresOnBoard[4].HasX = true;
            bool negativeResult = EndGame.NoLegalAggressiveMove(testGame);
            // Assert
            Assert.IsFalse(negativeResult);
            Assert.IsTrue(positiveResult);
        }
    }
}
