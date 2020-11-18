using System;
using Shobu3.GameLogic;
using Shobu3.Objects;

namespace Shobu3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Shobu!\n");
            Console.WriteLine(Game.rules);
            Console.WriteLine("Press enter to begin...");
            Console.ReadLine();
            PlayGame();
            Console.WriteLine("Thanks for playing!  Press enter to exit...");
            Console.ReadLine();
        }

        private static void PlayGame()
        {
            while (true)
            {
                Game game = new Game();
                game.RunGame();
                Console.WriteLine("Play again? (Y/N)");

                if (!(Console.ReadLine().ToUpper() == "Y"))
                {
                    return;
                }
            }
        }
    }
}
