using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;
using Shobu3.Objects;
using Shobu3;

namespace ShobuTests.ObjectTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MoveTests
    {
        [TestMethod]
        [DataRow(-1,4)]
        [DataRow(4,8)]
        [DataRow(5,10)]
        public void TransitionSquareIndexTest(int expected, int endIndex)
        {
            // Arrange
            Board testBoard = new Board(1);
            Move testMove = new Move(testBoard.SquaresOnBoard[0], testBoard.SquaresOnBoard[endIndex], testBoard, PlayerName.X, true);
            // Act
            int result = testMove.TransitionSquareIndex();
            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
