using System;
using Shobu3.GameLogic;
using Shobu3.Objects;

namespace Shobu3
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Game game = new Game();
                Console.WriteLine("Play again? (Y/N)");
                if (!(Console.ReadLine().ToUpper() == "Y"))
                {
                    break;
                }
            }
            Console.WriteLine("Thanks for playing!  Press enter to exit...");
            Console.ReadLine();
        }
    }
}
