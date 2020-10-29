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
    public class ConversionTests
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(7, 7)]
        [DataRow(10, 10)]
        public void ConvertSquareToBoardIndexTest(int expected, int testIndex)
        {
            // Arrange
            Board testBoard = new Board(1);
            // Act
            int result = Conversion.ConvertSquareToBoardIndex(testBoard.SquaresOnBoard[testIndex]);
            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(1, 1, 0)]
        [DataRow(2, 2, 5)]
        [DataRow(4, 1, 3)]
        public void ConvertXAndYToBoardIndexTest(int x, int y, int expected)
        {
            // Arrange
            // Act
            int result = Conversion.ConvertXAndYToBoardIndex(x, y);
            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("a3", 2)]
        [DataRow("b2", 5)]
        [DataRow("d4", 15)]
        public void ConvertLetterNumInputToBoardIndexTest(string input, int expected)
        {
            // Arrange
            Board testBoard = new Board(1);
            // Act
            int result = Conversion.ConvertLetterNumInputToBoardIndex(input);
            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
