using EscapeFromZaun.Model;
using System;
using System.Media;

namespace EscapeFromZaun.WpfLogic
{
    public class GameLogic : IGameLogic
    {
        public PlayerModel Player { get; set; }

        public GameLogic()
        {
            Player = new PlayerModel()
            {
                Name = "",
                Outcome = "Victory",
                PlayerRunTime = DateTime.Now
            };
        }
    }
}
