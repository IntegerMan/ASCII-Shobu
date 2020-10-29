using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shobu3;
using Shobu3.GameLogic;
using Shobu3.Objects;

namespace ShobuTests.ObjectTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void HasXBehavesAsExpected()
        {
            // Arrange
            Board testBoard = new Board(1);
            // Act
            bool shouldHaveX = testBoard.HasXs;
            for (int i = 0; i < 4; i++)
            {
                testBoard.SquaresOnBoard[i].HasX = false;
            }
            bool shouldNotHaveX = testBoard.HasXs;
            // Assert
            Assert.IsTrue(shouldHaveX);
            Assert.IsFalse(shouldNotHaveX);
        }

        [TestMethod]
        public void GetRowAsStringReturnsExpectedString()
        {
            // Arrange
            Board testBoard = new Board(1);
            // Act
            string result = testBoard.GetRowAsString(1);
            // Assert
            Assert.AreEqual("|X|X|X|X|", result);
        }
    }
}
