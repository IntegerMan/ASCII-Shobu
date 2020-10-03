using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.Objects;

namespace Shobu3.GameLogic
{
    /// <summary>
    /// Provides methods for determining winner and if the
    /// end game condition is met.
    /// </summary>
    public static class EndGame
    {
        public static bool BoardHasOnlyXsOrOs(Board[] boards)
        {
            foreach (Board board in boards)
            {
                if (!board.HasOs || !board.HasXs)
                {
                    return true;
                }
            }
            return false;
        }

        public static PlayerName DetermineWinner(Board[] boards)
        {
            foreach (Board board in boards)
            {
                if (BoardHasOnlyXsOrOs(boards))
                {
                    if (board.HasXs)
                    {
                        return PlayerName.X;
                    }
                    else
                    {
                        return PlayerName.O;
                    }
                }
            }
            Console.WriteLine("ERROR OCCURRED.  DETERMINEWINNER OR BOARDHASONLYXSOROS FAULTY");
            return PlayerName.X;
        }
    }
}
