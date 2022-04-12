using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.Logic
{
    public class GameLogic : IGameLogic
    {
        bool goingLeft = false;
        bool goingRight = false;
        bool jumping;
        int force = 20;
        int jumpSpeed = 10;
        public PlayerModel Player { get; set; }
        //PlayerLogic implementálás
        //Platformok listája

    }
}
