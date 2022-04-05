using EscapeFromZaun.Model;
using System;

namespace EscapeFromZaun.WpfLogic
{
    public class GameLogic : IGameLogic
    {
        bool goingLeft = false;
        bool goingRight = false;
        bool jumping;
        int force = 20;
        int jumpSpeed = 10;
        public PlayerModel Player { get; set; }
        public enum Directions { left,right }
        
    }
}
