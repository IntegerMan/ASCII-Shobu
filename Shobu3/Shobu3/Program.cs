using System;
using Shobu3.GameLogic;
using Shobu3.Objects;

namespace Shobu3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            while (game.GameIsDone == false)
            {
                game.TakeTurn();
            }
            Console.ReadLine();
        }
    }
}
