using System;
using System.Collections.Generic;
using System.Text;
using Shobu3.GameLogic;

namespace Shobu3.Objects
{
    /// <summary>
    /// Represents a player.  Holds their name, last passive move,
    /// and home boards.
    /// </summary>
    public class Player
    {
        public PlayerName Name { get; }
        public int[] HomeBoards { get; }
        public Move LastMoveMade { get; set; }
        public Player(PlayerName name, int[] homeBoards)
        {
            this.Name = name;
            this.HomeBoards = homeBoards;
        }

        public override string ToString()
        {
            return "Player " + Name;
        }
    }
}
