using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
    /// <summary>
    /// Represents an entire game: a series of turns until an end condition is met.
    /// </summary>
    public class Game
    {
        public Board[] mainBoards = new Board[4]
        {
            new Board(1),
            new Board(2),
            new Board(3),
            new Board(4)
        };

        public bool GameIsDone { get; set; }
        public Player PlayerX; 
        public Player PlayerO;
        public Player currentPlayer;

        public Game()
        {
            this.PlayerX = new Player(PlayerName.X, new int[2] { 1, 2 });
            this.PlayerO = new Player(PlayerName.O, new int[2] { 3, 4 });
            this.currentPlayer = PlayerX;
            this.GameIsDone = false;
            //RunGame();
        }
        /// <summary>
        /// Creates series of turns until end game condition is met.
        /// </summary>
        public void RunGame()
        {
            while (this.GameIsDone == false)
            {
                TakeTurn(currentPlayer);
                if (EndGame.BoardHasOnlyXsOrOs(mainBoards)) {
                    Console.WriteLine(EndGame.DetermineWinner(mainBoards) + " is the winner!");
                    GameIsDone = true;
                }
                if (currentPlayer == PlayerX)
                {
                    currentPlayer = PlayerO;
                }
                else
                {
                    currentPlayer = PlayerX;
                }
            }
        }

        public void TakeTurn(Player player)
        {
            Refresh();
            Turn turn = new Turn(this);
        }
        /// <summary>
        /// Refreshes console with updated piece positions
        /// </summary>
        public void Refresh()
        {
            string[] letterList = new string[4] { "A", "B", "C", "D" };
            Console.Clear();

            Console.WriteLine("                   Player X");
            Console.WriteLine("              1 2 3 4   1 2 3 4");

            PrintRowOfTwoBoards(mainBoards[0], mainBoards[1], 2, letterList);

            Console.WriteLine();

            PrintRowOfTwoBoards(mainBoards[2], mainBoards[3], 3, letterList);

            Console.WriteLine("              1 2 3 4   1 2 3 4");
            Console.WriteLine("                   Player O");
            Console.WriteLine();
        }

        static public string rules =
            "The goal of Shobu is to remove all of your opponents pieces on any ONE board.\n" +
            "On your turn, you first make a \"passive\" move, then an \"aggressive\" move.  The main\n" +
            "difference is that your passive move may not push any pieces, but the aggressive move may.\n\n" +
            "To make your passive move, select one of your home boards.  That is, one of the 2 boards on\n" +
            "your side of the screen.  Then, select one of your pieces, and choose where you would like\n" +
            "to move it.  You may move it horizontally, vertically, or diagonally up to 2 squares away,\n" +
            "but you may not push any pieces if it is your passive move.\n\n" +
            "Then for your aggressive move, you must choose one of the two boards of a DIFFERENT color\n" +
            "than the one you made your passive move on, but it does NOT have to be one of your home\n" +
            "boards.  It DOES have to move the same distance and direction as your passive move did.\n" +
            "This time, you may push one of your opponents pieces!  You may not push any of your own\n" +
            "pieces and you may not push 2 pieces with one move.  If you are able to push your opponent's\n" +
            "piece off the board, it is removed from the game.  If you remove all of your opponent's\n" +
            "pieces from one board, you win!\n\n" +
            "Finally, it is possible to run out of legal moves.  If that happens, you lose.\n\n" +
            "Enjoy!  And thanks for playing!\n";

        /// <summary>
        /// Prints 2 boards according to required colors and offsets, along with row labels
        /// </summary>
        private static void PrintRowOfTwoBoards(Board board1, Board board2, int boardNumOffset, string[] letterList)
        {
            for (int i = 1; i < 5; i++)
            {
                if (i == boardNumOffset)
                {
                    Console.Write($"Board {board1.BoardNumber}   ");
                }
                else
                {
                    Console.Write("          ");
                }
                Console.Write($" {letterList[i - 1]} ");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(board1.GetRowAsString(i));
                Console.ResetColor();
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(board2.GetRowAsString(i));
                Console.ResetColor();
                Console.Write($" {letterList[i - 1]}");
                if (i == boardNumOffset)
                {
                    Console.WriteLine($"    Board {board2.BoardNumber}");
                }
                else
                {
                    Console.WriteLine("          ");
                }
            }
        }
    }
}
